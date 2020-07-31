using System;
using System.IO;
using System.Xml.Linq;

namespace SamFirm
{
    internal static class Settings
    {
        //Get the temp folder path 
        private static string GetSettingsPath()
        {
            return Path.GetTempPath() + @"\Settings.xml";
        }

        //설정파일을 만드는 메소드
        private static void GenerateSettings()
        {
            string default_contents =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                "<SamFirm>\r\n" +
                    "\t<Model></Model>\r\n" +
                    "\t<Region></Region>\r\n" +
                    "\t<BinaryNature>False</BinaryNature>\r\n" +
                    "\t<AutoDecrypt>True</AutoDecrypt>\r\n" +
                "</SamFirm>";
            File.WriteAllText(GetSettingsPath(), default_contents);
        }

        //설정파일을 읽는 메소드
        public static string ReadSetting(string element)
        {
            string path = GetSettingsPath();
            try
            {
                if (!File.Exists(path))
                {
                    GenerateSettings();
                }
                return XDocument.Load(path).Element("SamFirm").Element(element).Value;
            }
            catch (Exception exception)
            {
                Logger.WriteLine("Error ReadSetting() -> " + exception);
                return string.Empty;
            }
        }

        //설정파일을 쓰는 메소드
        public static void SetSetting(string element, string value)
        {
            string path = GetSettingsPath();

            if (!File.Exists(path))
            {
                GenerateSettings();
            }
            XDocument document = XDocument.Load(path);
            XElement element2 = document.Element("SamFirm").Element(element);
            if (element2 == null)
            {
                document.Element("SamFirm").Add(new XElement(element, value));
            }
            else
            {
                element2.Value = value;
            }
            document.Save(path);
        }
    }
}