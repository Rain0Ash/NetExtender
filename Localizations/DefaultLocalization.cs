// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Reflection;
using NetExtender.Utils;
using NetExtender.Utils.Types;

namespace NetExtender.Localizations
{
    public class DefaultLocalization : DefaultLocalization<DefaultCultureStrings>
    {
        public DefaultLocalization(Int32 lcid)
            : base(lcid)
        {
        }
    }
    
    public class DefaultLocalization<T> : Localization where T : DefaultCultureStrings, new()
    {
        private static DefaultLocalization<T> localization;
        internal static DefaultLocalization<T> Localization
        {
            get
            {
                return localization ??= new DefaultLocalization<T>();
            }
        }

        // ReSharper disable once StaticMemberInGenericType
        private static readonly (Int32 Count, ConstructorInfo Constructor) Info;
        static DefaultLocalization()
        {
            ConstructorInfo info = typeof(T).GetConstructors().MaxBy(Analyze);
            Info = (Analyze(info), info);
        }

        private static Int32 Analyze(MethodBase info)
        {
            Int32 count = 0;
            foreach (ParameterInfo parameter in info.GetParameters())
            {
                if (parameter.ParameterType == typeof(String))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count;
        }
        
        public T Explorer { get; private set; }
        public T SelectAction { get; private set; }
        public T ViewAction { get; private set; }
        public T CopyAction { get; private set; }
        public T PasteAction { get; private set; }
        public T CutAction { get; private set; }
        public T AddAction { get; private set; }
        public T RemoveAction { get; private set; }
        public T ChangeAction { get; private set; }
        public T RecursiveAction { get; private set; }
        public T Exit { get; private set; }
        public T OK { get; private set; }
        public T Yes { get; private set; }
        public T No { get; private set; }
        public T Apply { get; private set; }
        public T Cancel { get; private set; }
        public T Retry { get; private set; }
        public T Ignore { get; private set; }
        public T Accept { get; private set; }
        public T Close { get; private set; }
        public T Select { get; private set; }
        public T Perform { get; private set; }
        public T Resume { get; private set; }
        public T Pause { get; private set; }
        public T Debug { get; private set; }
        public T Action { get; private set; }
        public T Good { get; private set; }
        public T Warning { get; private set; }
        public T CriticalWarning { get; private set; }
        public T Error { get; private set; }
        public T CriticalError { get; private set; }
        public T FatalError { get; private set; }
        public T UnknownError { get; private set; }
        public T FormatFileNameError { get; private set; }
        public T FormatDirectoryError { get; private set; }
        public T CreateDirectoryError { get; private set; }
        public T WriteAccessDeniedError { get; private set; }
        public T InitializedInvalidTaskError { get; private set; }
        public T StartedInvalidTaskError { get; private set; }
        public T Restart { get; protected set; }
        public T Shutdown { get; protected set; }
        public T ErrorRequestFailedTooManyTimes { get; protected set; }

        public DefaultLocalization()
            : base(new T())
        {
        }
        
        public DefaultLocalization(Int32 lcid)
            : base(lcid, new T())
        {
        }

        private static T Get(String english, String russian = null, String deutch = null)
        {
            return Invoke(english, russian, deutch);
        }
        
        private static T Invoke(params String[] strings)
        {
            // ReSharper disable once CoVariantArrayConversion
            return (T) Info.Constructor.Invoke(strings.Take(Info.Count).ToArray());
        }

        protected override void InitializeLanguage()
        {
            Explorer = Get(
                "Explorer",
                "Проводник");
            
            Restart = Get(
                "Restart",
                "Перезапустить",
                "Neustart");
            
            Shutdown = Get(
                "Exit",
                "Выйти",
                "Ausgang");

            ErrorRequestFailedTooManyTimes = Get(
                "Request failed after {0} attempts!",
                "Запрос окончился неудачей после {0} попыток!");
            
            Explorer = Get(
                "Explorer",
                "Проводник");

            SelectAction = Get(
                "Select",
                "Выбрать");

            ViewAction = Get(
                "View",
                "Просмотреть");

            CopyAction = Get(
                "Copy",
                "Копировать");

            PasteAction = Get(
                "Paste",
                "Вставить");

            CutAction = Get(
                "Cut",
                "Вырезать");

            AddAction = Get(
                "Add",
                "Добавить");

            RemoveAction = Get(
                "Remove",
                "Удалить");

            ChangeAction = Get(
                "Change",
                "Изменить");

            RecursiveAction = Get(
                "Recursive",
                "Рекурсивно");
            
            Exit = Get(
                "Exit",
                "Выход");

            OK = Get(
                "OK",
                "Понятно",
                "Ich verstehe");

            Yes = Get(
                "Yes",
                "Да",
                "Ja");

            No = Get(
                "No",
                "Нет",
                "Nein");

            Apply = Get(
                "Apply",
                "Применить");

            Cancel = Get(
                "Cancel",
                "Отменить",
                "Stornieren");

            Retry = Get(
                "Retry",
                "Повторить",
                "Wiederholen");

            Ignore = Get(
                "Ignore",
                "Игнорировать",
                "Ignorieren");

            Accept = Get(
                "Accept",
                "Принять",
                "Akzeptieren");

            Close = Get(
                "Close",
                "Закрыть",
                "Schließen");

            Select = Get(
                "Select",
                "Выбрать");

            Perform = Get(
                "Perform",
                "Выполнить",
                "Ausführen");

            Resume = Get(
                "Resume",
                "Возобновить",
                "Fortsetzen");

            Pause = Get(
                "Pause",
                "Приостановить",
                "Pause");

            Debug = Get(
                "Debug",
                "Отладка",
                "Debuggen");

            Action = Get(
                "Action",
                "Действие",
                "Aktion");

            Good = Get(
                "Good",
                "Хорошо",
                "Gut");

            Warning = Get(
                "Warning",
                "Предупреждение",
                "Warnung");

            CriticalWarning = Get(
                "Critical warning",
                "Критическое предупреждение",
                "Kritische warnung");

            Error = Get(
                "Error",
                "Ошибка",
                "Fehler");

            CriticalError = Get(
                "Critical error",
                "Критическая ошибка",
                "Kritischer fehler");

            FatalError = Get(
                "Fatal error",
                "Фатальная ошибка",
                "Fataler fehler");

            UnknownError = Get(
                "Unknown error",
                "Неизвестная ошибка",
                "Unbekannter fehler");
            
            FormatFileNameError = Get(
                "Format filename error",
                "Ошибка при сборке имени файла");

            FormatDirectoryError = Get(
                "Format directory error",
                "Ошибка при сборке имени директории");

            CreateDirectoryError = Get(
                "Error on create directory",
                "Ошибка при создании директории",
                "Fehler beim Erstellen des Verzeichnisses");

            WriteAccessDeniedError = Get(
                "No write access on this path",
                "Отсутствует доступ на запись по данному пути",
                "Kein Schreibzugriff auf diesen Pfad");

            InitializedInvalidTaskError = Get(
                "An attempt was made to initialize an invalid task",
                "Была предпринята попытка инициализировать неверную задачу",
                "Es wurde versucht, eine ungültige Aufgabe zu initialisieren");

            StartedInvalidTaskError = Get(
                "An attempt was made to start an invalid task",
                "Была предпринята попытка запустить неверную задачу",
                "Es wurde versucht, eine ungültige Aufgabe zu starten");
        }
    }
}