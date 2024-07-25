using BusinessAccessLayer.DTOS.AddressDtos;
using BusinessAccessLayer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services.AddressService
{
    public interface IAddressService
    {
        Task<CreateStatus<CreateAddressDto>> CreateAddress(CreateAddressDto createAddressDto);
        Task<List<ViewAddressDto>>? ViewAddressByUserId(string userId);
    }
}
