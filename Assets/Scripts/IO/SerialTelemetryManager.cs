using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System.Collections.Concurrent;

public class SerialTelemetryManager : MonoBehaviour
{
    [Header("Configurações de Hardware")]
    [SerializeField] private string portName = "COM3";
    [SerializeField] private int baudRate = 115200;

    // Fila thread-safe para armazenar o último dado a ser enviado
    // Usamos ConcurrentQueue para evitar race conditions sem lock pesado
    private ConcurrentQueue<string> _sendQueue = new ConcurrentQueue<string>();

    private SerialPort _serialPort;
    private Thread _serialThread;
    private bool _isRunning = false;

    private void Start()
    {
        StartSerialThread();
    }

    private void StartSerialThread()
    {
        _serialPort = new SerialPort(portName, baudRate)
        {
            ReadTimeout = 10,
            WriteTimeout = 10,
            DtrEnable = true // Importante para alguns modelos de Arduino Uno
        };

        try
        {
            _serialPort.Open();
            _isRunning = true;

            // Iniciamos a Thread dedicada
            _serialThread = new Thread(SerialWriteLoop)
            {
                IsBackground = true,
                Priority = System.Threading.ThreadPriority.Normal
            };
            _serialThread.Start();

            Debug.Log($"[Serial] Conectado em {portName} a {baudRate} baud.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[Serial] Erro ao abrir porta: {e.Message}");
        }
    }

    // O Loop que roda fora da Main Thread do Unity
    private void SerialWriteLoop()
    {
        while (_isRunning && _serialPort != null && _serialPort.IsOpen)
        {
            // Verificamos se há algo novo para enviar
            if (_sendQueue.TryDequeue(out string dataToSend))
            {
                try
                {
                    // Enviamos o dado com o terminador \n exigido pelo seu buffer circular
                    _serialPort.WriteLine(dataToSend);
                }
                catch (System.TimeoutException) { /* Ignora timeout de escrita */ }
                catch (System.Exception e)
                {
                    Debug.LogError($"[Serial] Erro de escrita: {e.Message}");
                }
            }

            // Sleep curto para não fritar o núcleo da CPU desnecessariamente
            // 10ms = 100hz de atualização, mais que suficiente para o OLED
            Thread.Sleep(10);
        }
    }

    // Método público para ser chamado pelo seu PlayerController
    public void SendSpeed(string speed)
    {
        if (!_isRunning) return;

        // Limpamos a fila antes de inserir para garantir que o Arduino
        // sempre receba a velocidade MAIS RECENTE (LIFO behavior simulado)
        while (_sendQueue.Count > 0) _sendQueue.TryDequeue(out _);

        _sendQueue.Enqueue(speed);
    }

    private void OnDisable()
    {
        StopThread();
    }

    private void OnDestroy()
    {
        StopThread();
    }

    private void StopThread()
    {
        _isRunning = false;

        if (_serialThread != null && _serialThread.IsAlive)
            _serialThread.Join(500);

        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.Close();
            _serialPort = null;
        }
        Debug.Log("[Serial] Conexão encerrada com segurança.");
    }
}