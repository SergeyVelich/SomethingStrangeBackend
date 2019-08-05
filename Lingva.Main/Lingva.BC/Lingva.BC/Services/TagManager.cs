using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class TagManager : ITagManager
    {
        private readonly ITagRepository _tagRepository;
        private readonly IDataAdapter _dataAdapter;
           
        public TagManager(ITagRepository tagRepository, IDataAdapter dataAdapter)
        {
            _tagRepository = tagRepository;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<TagDto>> GetListByPostAsync(int id)
        {
            IEnumerable<Tag> tags = await _tagRepository.GetListByPostAsync(id);

            return _dataAdapter.Map<IEnumerable<TagDto>>(tags);
        }      
    }
}

