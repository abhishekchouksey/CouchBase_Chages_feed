using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    /// <summary>
    /// Used to access and manage documents. If you want to work with entities, POCOs,
    /// use <see cref="IEntities"/> instead, via <see cref="IMyCouchClient.Entities"/>.
    /// </summary>
    public interface IDocuments
    {
        //ISerializer Serializer { get; }

        //Task<BulkResponse> BulkAsync(BulkRequest request);

        //Task<DocumentHeaderResponse> CopyAsync(string srcId, string newId);

        //Task<DocumentHeaderResponse> CopyAsync(string srcId, string srcRev, string newId);

        //Task<DocumentHeaderResponse> CopyAsync(CopyDocumentRequest request);

        //Task<DocumentHeaderResponse> ReplaceAsync(string srcId, string trgId, string trgRev);

        //Task<DocumentHeaderResponse> ReplaceAsync(string srcId, string srcRev, string trgId, string trgRev);

        //Task<DocumentHeaderResponse> ReplaceAsync(ReplaceDocumentRequest request);

        //Task<DocumentHeaderResponse> HeadAsync(string id, string rev = null);

        //Task<DocumentHeaderResponse> HeadAsync(HeadDocumentRequest request);

        ///// <returns>Untyped response with JSON.</returns>
        //Task<DocumentResponse> GetAsync(string id, string rev = null);

        //Task<DocumentResponse> GetAsync(GetDocumentRequest request);

        //Task<DocumentHeaderResponse> PostAsync(string doc);

        //Task<DocumentHeaderResponse> PostAsync(PostDocumentRequest request);

        //Task<DocumentHeaderResponse> PutAsync(string id, string doc);
        
        //Task<DocumentHeaderResponse> PutAsync(string id, string rev, string doc);

       
        //Task<DocumentHeaderResponse> PutAsync(PutDocumentRequest request);

       
        //Task<DocumentHeaderResponse> DeleteAsync(string id, string rev);

       
        //Task<DocumentHeaderResponse> DeleteAsync(DeleteDocumentRequest request);

        //Task<RawResponse> ShowAsync(QueryShowRequest request);
    }
}
