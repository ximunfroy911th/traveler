using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace Traveler.Services;

interface IFirestoreService
{
    Task<DocumentSnapshot> GetDocumentAsync(string collectionPath, string documentId);
    Task<QuerySnapshot> GetCollectionAsync(string collectionPath);
    Task AddDocumentAsync(string collectionPath, string documentId, Dictionary<string, object> data);
    Task UpdateDocumentAsync(string collectionPath, string documentId, Dictionary<string, object> data);
    Task DeleteDocumentAsync(string collectionPath, string documentId);
}
public class FirestoreService: IFirestoreService
{
    private readonly FirestoreDb _db;

    public FirestoreService(IServiceProvider serviceProvider)
    {
        var app = FirebaseApp.DefaultInstance ?? FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("defend-9814c-firebase-adminsdk-urwld-b3c02a3eaf.json"),
        });
        // _db = FirestoreDb.Create(app);
    }
    public async Task<DocumentSnapshot> GetDocumentAsync(string collectionPath, string documentId)
    {
        DocumentReference docRef = _db.Collection(collectionPath).Document(documentId);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            Console.WriteLine("Document data for {0} document:", snapshot.Id);
            Dictionary<string, object> city = snapshot.ToDictionary();
            foreach (KeyValuePair<string, object> pair in city)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }

            return snapshot;
        }
        else
        {
            Console.WriteLine("Document {0} does not exist!", documentId);
            return null;
        }
    }

    public async Task<QuerySnapshot> GetCollectionAsync(string collectionPath)
    {
        CollectionReference citiesRef = _db.Collection(collectionPath);
        QuerySnapshot snapshot = await citiesRef.GetSnapshotAsync();
        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            Console.WriteLine("Document data for {0} document:", document.Id);
            Dictionary<string, object> city = document.ToDictionary();
            foreach (KeyValuePair<string, object> pair in city)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
        }

        return snapshot;
    }

    public async Task AddDocumentAsync(string collectionPath, string documentId, Dictionary<string, object> data)
    {
        DocumentReference docRef = _db.Collection(collectionPath).Document(documentId);
        await docRef.SetAsync(data);
        Console.WriteLine("Added document with ID: {0}", documentId);
    }

    public async Task UpdateDocumentAsync(string collectionPath, string documentId, Dictionary<string, object> data)
    {
        DocumentReference docRef = _db.Collection(collectionPath).Document(documentId);
        await docRef.UpdateAsync(data);
        Console.WriteLine("Updated document with ID: {0}", documentId);
    }

    public async Task DeleteDocumentAsync(string collectionPath, string documentId)
    {
        DocumentReference docRef = _db.Collection(collectionPath).Document(documentId);
        await docRef.DeleteAsync();
        Console.WriteLine("Deleted document with ID: {0}", documentId);
    }
}