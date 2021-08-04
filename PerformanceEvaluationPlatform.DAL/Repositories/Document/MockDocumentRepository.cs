using PerformanceEvaluationPlatform.DAL.Models.Documents.Dto;
using PerformanceEvaluationPlatform.DAL.Models.Documents.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Documentdoc = PerformanceEvaluationPlatform.DAL.Models.Documents.Dao.Document;
using PerformanceEvaluationPlatform.DAL.Models.Documents.Mappers;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Document
{
    // Mock Repository created for testing and simulate behaviour of database 
    // This repository not injected in Project 
    // Just for personal use

    public class MockDocumentRepository : IDocumentReposotory
    {
        private readonly HashSet<Documentdoc> _documents=new HashSet<Documentdoc>();
        private readonly HashSet<Models.Documents.Dao.DocumentType> _documentTypes=new HashSet<DocumentType>();

        public MockDocumentRepository()
        {
            _documents.UnionWith(Enumerable.Range(1, 3).Select(x => new Documentdoc
            {
                Id = x,
                UserId = x,
                TypeId = x,
                ValidToDate = DateTime.Now.AddDays(x),
                FileName = $"File {x}",
                //CreatedAt = DateTime.Now.AddDays(-x),
                CreatedById = x,
                MetaData = $"Some Random strign {x}"
            }));
            //Creating Data for Types
            _documentTypes.UnionWith(Enumerable.Range(1, 3).Select(x => new DocumentType
            {
                Id = x,
                Name = $"Document Type {x}"

            }));
        }
        public async Task DeleteDocument(int id)
        {
            var docModel = _documents.FirstOrDefault(x => x.Id == id);
            await Task .Run(() => _documents.Remove(docModel));
            
            
        }

        public async Task<DocumentDetailDto> GetDocument(int id)
        {
            var docum = _documents.FirstOrDefault(x => x.Id == id);
            
            if (docum == null) {
                return null;
            }
            var docType = _documentTypes.FirstOrDefault(x => x.Id == docum.TypeId);
            if (docType == null) {
                return null;
            }

            return await Task.Run(() => {
                var documentDto = new DocumentDetailDto()
                {
                    Id = docum.Id,
                    FirstName = $"docum.UserId",
                    LastName = $"docum.UserId",
                    DocumentType = docType.Name,
                    FileName = docum.FileName,
                    ValidToDate = docum.ValidToDate,
                    CreatedByFirstName = $"docum.CreatedById",
                    CreatedByLastName = $"docum.CreatedById",
                    CreatedAt = docum.CreatedAt,
                    LastUpdatesByFirstName = docum.LastUpdatesById?.ToString(),
                    LastUpdatesByLastName = docum.LastUpdatesById?.ToString(),
                    LastUpdatesAt = docum.LastUpdatesAt

                };
                return documentDto;
            });
        }

        public async Task<IList<DocumentListItemDto>> GetDocuments(DocumentListFilterDto filter)
        {
            CheckSkipTakeFiltration(filter);
            IEnumerable<Documentdoc> documents = _documents;
            if (!string.IsNullOrWhiteSpace(filter.Search) || !string.IsNullOrEmpty(filter.Search))
            {
                documents = documents.Where(x => x.FileName.Contains(filter.Search)).ToList();
            }
            if (filter.UserIds != null)
            {   //TODO: Reafctor after addit user repository

                var tempdoc = new List<Documentdoc>();
                foreach (var userId in filter.UserIds)
                {
                    tempdoc.AddRange(documents.Where(x => x.UserId == userId));
                }
                documents = tempdoc;
            }
            if (filter.TypeIds != null)
            {
                var typemodel = new List<DocumentType>();
                foreach (var type in filter.TypeIds)
                {
                    typemodel.Add(_documentTypes.FirstOrDefault(x => x.Id == type));
                }
                var tempdoc = new List<Documentdoc>();
                foreach (var type in typemodel)
                {
                    foreach (var doc in documents)
                    {
                        if (type != null)
                        {
                            if (doc.TypeId == type.Id)
                            {
                                tempdoc.Add(doc);
                            }
                        }
                    }
                }
                documents = tempdoc;

            }
            if (filter.ValidTo != null && filter.ValidTo > default(DateTime))
            {
                documents = documents.Where(x => x.ValidToDate < filter.ValidTo).ToList();
            }
            //ordering by category
            documents = CheckOrderforFiltration(documents, filter);

            if (documents.ToList().Count() > 30)
            {
                documents = documents.Skip(filter.Skip.Value).Take(filter.Take.Value).ToList();
            }
            return await Task.Run(() => {
                var documentListDto = new HashSet<DocumentListItemDto>();
                foreach (var doc in documents)
                {
                    var docListItemDto = new DocumentListItemDto()
                    {
                        Id = doc.Id,
                        ValidToDate = doc.ValidToDate,
                        FirstName = $"{doc.UserId}",
                        LastName = $"{doc.UserId}",
                        FileName = doc.FileName,
                    };
                    var type = _documentTypes.FirstOrDefault(t => t.Id == doc.TypeId);
                    docListItemDto.DocumentTypeName = type.Name;
                    documentListDto.Add(docListItemDto);
                }
;
                return documentListDto.ToList();
            });
            
        }

        public async Task<DocumentTypeDto> GetTypeModel(int id)
        {
            return await Task.Run(() => {
                var doctype = _documentTypes.FirstOrDefault(x => x.Id == id);

                if (doctype == null)
                {
                    return null;
                }
                var docTypeDto = doctype.AsDocumentTypeDto();
                return docTypeDto;
            });
        }

        //for mockingdata filtertion
        private void CheckSkipTakeFiltration(DocumentListFilterDto filter)
        {
            if (filter.Skip == null)
            {
                filter.Skip = 0;
            }
            if (filter.Take == null)
            {
                filter.Take = 30;
            }
        }

        private IEnumerable<Documentdoc> CheckOrderforFiltration(IEnumerable<Documentdoc> docum, DocumentListFilterDto filer)
        {
            if (filer.TypeSortOrder.HasValue)
            {
                if (filer.NameSortOrder.HasValue)
                {
                    return orderByCategoryType(docum, filer.TypeSortOrder.Value, filer.NameSortOrder.Value);
                }
            }
            return docum;
        }
        private IEnumerable<Documentdoc> orderByCategoryType(IEnumerable<Documentdoc> doc, int? categy, int? order)
        {
            if (categy == 1)
            {

                if (order == 1)
                {
                    return doc.OrderBy(x => x.UserId).ToList();

                }
                else
                {
                    return doc.OrderByDescending(x => x.UserId).ToList();
                }
            }
            else if (categy == 2)
            {
                if (order == 1)
                {
                    return doc.OrderBy(x => x.TypeId).ToList();

                }
                else
                {
                    return doc.OrderByDescending(x => x.TypeId).ToList();
                }
            }
            return doc;
        }

        public Task SaveChanges()
        {
            return Task.CompletedTask;
        }

        public async Task<Documentdoc> Get(int id)
        {   
            return await Task.Run(() => _documents.FirstOrDefault(x => x.Id == id));
        }

        public async Task Create(Documentdoc example)
        {
             await Task.Run(()=> _documents.Add(example));
            
            
        }

        public async Task<IList<DocumentTypeDto>> GetTypes()
        {
             

            return await Task.Run(() =>
            {
                var docTypes = _documentTypes.ToList();
                var docTypesDtos = Mapper.ConvertToIEnumerableDocumetTypeDtoFromIEnumerableDocumentType(docTypes);
                return docTypesDtos;
            });
        }
    }
}
