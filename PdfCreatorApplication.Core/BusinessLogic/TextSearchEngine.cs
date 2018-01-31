using System;
using System.Diagnostics;
using System.IO;

namespace PdfCreatorApplication.Core.BusinessLogic
{
    public class TextSearchEngine : ISearchable
    {
        // Используется для выяснения того, вызывался ли уже метод Dispose()
        private bool _disposed = false;

        private static volatile TextSearchEngine _instance = new TextSearchEngine();
        private static readonly object Locker = new Object();

        private MemoryStream _stream = new MemoryStream();

        /// <summary>
        /// Checks the disposing.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException"></exception>
        private void CheckDisposing()
        {
            if (_disposed) throw new ObjectDisposedException(GetType().ToString());
        }

        public static TextSearchEngine Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new TextSearchEngine();
                        }
                    }
                }

                return _instance;
            }
        }

        private TextSearchEngine()
        {
            Debug.WriteLine("TextSearchEngine: init()");
        }

        public void Search()
        {
            CheckDisposing();
            Debug.WriteLine("TextSearchEngine: Search()");
        }

        public void Refresh()
        {
            CheckDisposing();
            Debug.WriteLine("TextSearchEngine: Refresh()");
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TextSearchEngine"/> class.
        /// </summary>
        ~TextSearchEngine()
        {
            // Вызов вспомогательного метода.
            // Значение false указывает на то, что
            // очистка была инициирована сборщиком мусора.
            Cleanup(false);
            Debug.WriteLine("TextSearchEngine: Bye!");
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        /// <param name="forcedDisposing">if set to <c>true</c> make forced disposing.</param>
        private void Cleanup(bool forcedDisposing)
        {
            // Проверка, выполнялась ли очистка,
            if (!_disposed)
            {
                try
                {
                    // Если disposing равно true, должно осуществляться
                    // освобождение всех управляемых ресурсов,
                    if (forcedDisposing)
                    {
                        // -------------------------------------------------------
                        // Здесь осуществляется освобождение управляемых ресурсов.
                        // -------------------------------------------------------
                        // Управляемые ресурсы - это все типы в CLR (все примитивные, все ссылочные типы/типы значения, 
                        // экземпляры классов, структуры и прочее). Условно говоря, располагаются в управляемой памяти 
                        // и могут быть собраны сборщиком мусора автоматически.
                        _instance = null;

                        Debug.WriteLine("TextSearchEngine: managed resources disposed...");
                    }
                    // -------------------------------------------------------
                    // В этой части очищаем неуправляемые ресурсы, 
                    // вызовом у таких объектов методов Dispose() or Flush() 
                    // для потока, или ч.л. подобное
                    // -------------------------------------------------------
                    // Неуправляемые ресурсы - всё, что не относится к CLR (хэндлы файлов, хэндлы GDI, 
                    // блоки выделенной памяти, а также всё, с чем работают функции из подключенных неуправляемых 
                    // библиотек через DllImport). 
                    // Сборщик мусора не знает об этих объектах и не может их высвобождать автоматически - 
                    // это нужно делать вручную.
                    if (_stream != null)
                    {
                        _stream.Close();
                        _stream.Dispose();
                        _stream = null;
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("TextSearchEngine: ERROR! " + ex);
                }
                Debug.WriteLine("TextSearchEngine: unmanaged resources disposed...");
            }
            _disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Вызов вспомогательного метода.
            // Значение true указывает на то, что очистка
            // была инициирована пользователем объекта.
            Cleanup(true);
            // Подавление финализации.
            GC.SuppressFinalize(this);
            //
            Debug.WriteLine("TextSearchEngine: Disposed");
        }


    }
}
