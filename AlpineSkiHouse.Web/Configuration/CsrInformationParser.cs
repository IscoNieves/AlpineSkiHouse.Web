using System;
using System.Collections.Generic;
using System.IO;

namespace AlpineSkiHouse.Web.Configuration
{
    public class CsrInformationParser
    {
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public IDictionary<string, string> Parse(Stream stream)
        {
            _data.Clear();

            using (var reader = new StreamReader(stream))
            {
                // First line contains the phone number
                var line = reader.ReadLine();
                var phoneNumber = ExtractPhoneNumber(line);

                // All subsequent lines contain contact information
                var onlineCount = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    onlineCount++;
                }

                // Assign to the config store
                _data.Add("CrsInformationOptions:PhoneNumber", phoneNumber);
                _data.Add("CrsInformationOptions:OnlineRepresentatives", onlineCount.ToString());
            }

            return _data;
        }

        private string ExtractPhoneNumber(string input)
        {
            var phoneNumberInfo = input.Split('|');
            var result = phoneNumberInfo[1];

            return result;
        }
    }
}
