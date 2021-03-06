using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Models.CarVM;
using RCAR.Models.PaymentRecordVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateCarAsync(CarCreateVM carVM, int memberId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (!user.Members.Where(m => m.MemberId == carVM.MemberId && !m.IsRemoved).Any())
            {
                var car = new Car()
                {
                    Brand = carVM.Brand,
                    Model = carVM.Model,
                    Description = carVM.Description,
                    ServiceSince = carVM.ServiceSince,
                    ServiceTo = carVM.ServiceTo,
                    Status = carVM.Status,
                    MemberId = memberId
                };
                _unitOfWork.Car.Add(car);
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;
        }

        public async Task<CarDetailVM> DetailCarAsync(int carId)
        {
            var car = await _unitOfWork.Car.GetByIdAsync(carId);
            var model = _mapper.Map<Car, CarDetailVM>(car);
            return model;
        }

        public async Task<bool> DoneCarSeriveAsync(int carId)
        {
            var car = await _unitOfWork.Car.GetByIdAsync(carId);
            car.Status = "Zakończony";
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> InProgressCarStatusAsync(int carId)
        {
            var car = await _unitOfWork.Car.GetByIdAsync(carId);
            car.Status = "W trakcie";
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> RemoveCarAsync(int carId)
        {
            var car = await _unitOfWork.Car.GetByIdAsync(carId);
            _unitOfWork.Car.Remove(car);
            return await _unitOfWork.SaveChangesAsync();
        }

        public void CountPayment(IEnumerable<PaymentVM> model, ref int count, ref decimal totalPayment)
        {
            count = model.Count();
            totalPayment = model.Sum(p => p.TotalAmount);
        }
    }
}
