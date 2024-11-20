using Supplier.Application.DTOs.Companys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supplier.Application.Services
{
    public interface ICompanyService
    {
        void Create(CompanyRequestDto requestDto);
        Task Update(Guid id, CompanyRequestDto requestDto);
        Task Delete(Guid id);
        Task<IList<CompanyResponseDto>> GetAll();
        Task<CompanyResponseDto> GetById(Guid id);
    }
}
