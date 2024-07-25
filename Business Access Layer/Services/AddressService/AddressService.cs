using AutoMapper;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.AddressDtos;
using DataAccessLayer.Data.Models;
using DataAccessLayer.UnitOfWorkRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services.AddressService
{
    internal class AddressService : IAddressService
    {
        public IUnitOfWork unitOfWork;
        public IMapper mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        #region create Address
        public async Task<CreateStatus<CreateAddressDto>> CreateAddress(CreateAddressDto createAddressDto)
        {
            // map . 
            Address address = mapper.Map<Address>(createAddressDto);
            // save . 
            await unitOfWork.addressRepo.AddAsync(address);
            int result = unitOfWork.Complete();
            // cheack save . 
            if (result == 1)
            {
                return new CreateStatus<CreateAddressDto>
                {
                    Status = true,
                    Massage = "Adress added",
                    Value = createAddressDto

                };
            }
            return new CreateStatus<CreateAddressDto>
            {
                Status = false,
                Massage = "try again"
            };
            // return 

        }
        #endregion

        public async Task<List<ViewAddressDto>>? ViewAddressByUserId(string userId)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread ID before0 awaiting: {threadId}");
            var address = await unitOfWork.addressRepo.GetAddressByUserId(userId);
            if (address == null)
            {
                return new List<ViewAddressDto>();
            }
            var result = mapper.Map<List<ViewAddressDto>>(address);
            return result; 

        }
    }
}
