﻿using RCAR.Models.CarVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface ICarService
    {
        Task<bool> CreateCarAsync(CarCreateVM carVM, int memberId, string userId);
        Task<bool> RemoveCarAsync(int carId);
        Task<bool> DoneCarSeriveAsync(int carId);
        Task<CarDetailVM> DetailCarAsync(int carId);
    }
}
