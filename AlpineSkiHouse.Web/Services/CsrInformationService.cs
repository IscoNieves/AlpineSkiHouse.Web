﻿using AlpineSkiHouse.Web.Configuration.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlpineSkiHouse.Web.Services
{
    public class CsrInformationService : ICsrInformationService
    {
        private ILogger<CsrInformationService> _logger;
        private CsrInformationOptions _options;

        public CsrInformationService(IOptions<CsrInformationOptions> options, ILogger<CsrInformationService> logger)
        {
            _options = options.Value;
            _logger = logger;

            logger.LogInformation("Entered the CsrInformationService constructor");
        }

        public bool CallCenterOnline
        {
            get
            {
                return _options.OnlineRepresentatives > 0;
            }
        }

        public string CallCenterPhoneNumber
        {
            get
            {
                return _options.PhoneNumber;
            }
        }

        public int OnlineRepresentatives
        {
            get
            {
                return _options.OnlineRepresentatives;
            }
        }
    }
}
