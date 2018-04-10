using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{/// <summary>
 /// Used to access and manage attachments to documents.
 /// </summary>
    public interface IAttachments
    {
       // Task<AttachmentResponse> GetAsync(string docId, string attachmentName);

       //Task<AttachmentResponse> GetAsync(string docId, string docRev, string attachmentName);
        
       // Task<AttachmentResponse> GetAsync(GetAttachmentRequest request);

        
       // Task<DocumentHeaderResponse> PutAsync(PutAttachmentRequest request);

       // Task<DocumentHeaderResponse> DeleteAsync(string docId, string docRev, string attachmentName);

        
       // Task<DocumentHeaderResponse> DeleteAsync(DeleteAttachmentRequest request);
    }
}
