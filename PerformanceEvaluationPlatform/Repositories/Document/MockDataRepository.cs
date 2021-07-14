using PerformanceEvaluationPlatform.Models.Document.BaseModels;
using PerformanceEvaluationPlatform.Models.Document.Mappers;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using PerformanceEvaluationPlatform.Models.Document.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Repositories.Document
{
    //TODO: Rebuild after add some new DTO
    // Simulate  DataBase Context class.
    public class MockDataRepository : IDocumentReposotory
    {
        private HashSet<DocumentModel> _documents = new HashSet<DocumentModel>();
        private HashSet<TypeModel> _types = new HashSet<TypeModel>();
        public MockDataRepository() {
            //Creating Mock Data of Document
            Random rand = new Random();
            _documents.UnionWith(Enumerable.Range(1, 50).Select(x => new DocumentModel
            {
                Id = x,
                UserId = x + 1,
                TypeId = rand.Next(1,5),
                ValidToDate = DateTime.Now.AddDays(x),
                FileName = $"File{x}",
                CreatedAt= DateTime.Now.AddDays(-x),
                CreatedById= x,
                LastUpdatesAt= DateTime.Now,
                LastUpdateById= x + 1,
                MetaData = $"Some Random strign {x}"
            })) ;
            //Creating Data for Types
            _types.UnionWith(Enumerable.Range(1, 5).Select(x => new TypeModel
            {
                Id = x,
                Name = $"Document Type {x}"

            }));
        }

        public bool DeleteDocument(DocumentModel document)
        {
            if (_documents.Contains(document)) {
                _documents.Remove(document);
                return true;
            }
            return false;
        }

        public bool DeleteType(TypeModel type)
        {
            if (_types.Contains(type)) {
                _types.Remove(type);
                return true;
            }
            return false;
        }

        public IEnumerable<DocumentModel> GetDocuments()
        {
            return _documents;
        }

        public DocumentModel GetDocument(int id) {
           return _documents.FirstOrDefault(x=>x.Id==id) ;
        }

        public IEnumerable<TypeModel> GetTypes()
        {
            return _types;
        }

        public TypeModel GetTypeModel(int id) {
            return _types.FirstOrDefault(x=>x.Id==id);
        }

        public bool SaveDocument(DocumentModel model)
        {
            var typeExist = _types.Select(x => x.Id == model.TypeId);
            if (typeExist != null)
            {
                model.Id=_documents.Select(x => x.Id).LastOrDefault()+1;
                model.CreatedAt = DateTime.Now;
                _documents.Add(model);
                return true;
            }
            else {
                return false;
            }
        }

        public bool UpdateDocument(DocumentModel model)
        {
            var currentDoc=_documents.Select(x=>x).Where(d=>d.Id==model.Id).FirstOrDefault();
            if (currentDoc != null)
            {
                _documents.Remove(currentDoc);
                _documents.Add(model);
                return true;
            }
            return false;

        }

        public bool UpdateType(TypeModel type)
        {
            var currType = _types.Select(t => t).Where(t => t.Id == type.Id).FirstOrDefault();
            if (currType != null) {
                _types.Remove(currType);
                _types.Add(type);
                return true;
            }
            return false;
        }

        public IEnumerable<DocumentModel> GetFiltredDocumentsLits(DocumentRequestModel filter)
        {
            CheckSkipTakeFiltration(filter);
            IEnumerable<DocumentModel> documents = _documents;
            if (!string.IsNullOrWhiteSpace(filter.Search) || !string.IsNullOrEmpty(filter.Search))
            {
                documents = documents.Where(x => x.FileName.Contains(filter.Search)).ToList();
            }
            if (filter.UserIds != null)
            {   //TODO: Reafctor after addit user repository

                var tempdoc = new List<DocumentModel>();
                foreach (var userId in filter.UserIds) {
                    tempdoc.AddRange(documents.Where(x => x.UserId == userId));
                }
                documents = tempdoc;
            }
            if (filter.TypeIds != null)
            {
                var typemodel = new List<TypeModel>();
                foreach (var type in filter.TypeIds) {
                    typemodel.Add(_types.FirstOrDefault(x => x.Id == type));
                }
                var tempdoc = new List<DocumentModel>();
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

            if (documents.ToList().Count() > 30) {
                documents=documents.Skip(filter.Skip.Value).Take(filter.Take.Value).ToList();
            }
            return documents;
        }

        private void CheckSkipTakeFiltration(DocumentRequestModel filter) {
            if (filter.Skip == null) {
                filter.Skip = 0;
            }
            if (filter.Take == null) {
                filter.Take = 30;
            }
        }

        private IEnumerable<DocumentModel> CheckOrderforFiltration(IEnumerable<DocumentModel> docum, DocumentRequestModel filer) {
            if (filer.SortCategy.HasValue) {
                if (filer.SortOrder.HasValue) {
                   return orderByCategoryType(docum, filer.SortCategy.Value, filer.SortOrder.Value);
                }
            }
            return docum;
        }
        private IEnumerable<DocumentModel> orderByCategoryType(IEnumerable<DocumentModel> doc, SortCategy categy, SortOrder order) {
            if (categy == SortCategy.Name) {

                if (order == SortOrder.Ascending)
                {
                    return doc.OrderBy(x => x.UserId).ToList();
                    
                }
                else {
                    return doc.OrderByDescending(x => x.UserId).ToList();
                }
            }else if (categy == SortCategy.Type) {
                if (order == SortOrder.Ascending)
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
    }
}
