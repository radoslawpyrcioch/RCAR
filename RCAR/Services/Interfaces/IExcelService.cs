﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IExcelService
    {
        Task<byte[]> GenerateReportServiceExcel(string userId);
    }
}