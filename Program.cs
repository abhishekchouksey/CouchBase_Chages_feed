using Couchbase;
using Couchbase.Configuration.Client;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Couchbase.Management;

namespace ChangeFeeds
{
    class Program
    {
        static Dictionary<string, List<Change>> conflicts = new Dictionary<string, List<Change>>();
        static void Main(string[] args)
        {

            // MainSync();
            // MainSync2();
            // MainSync3();

            //Thread workerThread = new Thread(ProcessConflictResolve);
            //workerThread.Start();


            var config = new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri("http://localhost:8091/") }
            };



            using (var cluster = new Cluster(config))
            {
                Console.WriteLine("Authenticating as administrator.");
                cluster.Authenticate("Admin", "Admin@1");

                // Open the couchbasegames bucket.
                //
                var bucket = cluster.OpenBucket("couchbasegames");

                // Add key-value pairs to hotel_10138, representing traveller-Ids and associated discount percentages.
                //
                bucket.MutateIn<dynamic>("travel::2")
                    .Upsert("discounts.jsmith123", "20", SubdocPathFlags.CreatePath | SubdocPathFlags.Xattr)
                    .Upsert("discounts.pjones356", "30", SubdocPathFlags.CreatePath | SubdocPathFlags.Xattr)

                    // The following lines, "insert" and "remove", simply demonstrate insertion and
                    // removal of the same path and value.
                    //
                    .Insert("discounts.jbrown789", "25", SubdocPathFlags.CreatePath | SubdocPathFlags.Xattr)
                    .Remove("discounts.jbrown789", SubdocPathFlags.Xattr)

                    .Execute();

                // Add key-value pairs to hotel_10142, again representing traveller-Ids and associated discount percentages.
                //
                bucket.MutateIn<dynamic>("travel::1")
                    .Upsert("discounts.jsmith123", "15", SubdocPathFlags.CreatePath | SubdocPathFlags.Xattr)
                    .Upsert("discounts.pjones356", "10", SubdocPathFlags.CreatePath | SubdocPathFlags.Xattr)
                    .Execute();



                bucket.MutateIn<dynamic>("travel::3")
                  .Upsert("discounts.jsmith123", "15")
                  .Upsert("discounts.pjones356", "10")
                  .Execute();
                // Create a user and assign roles. This user will search for their
                // available discounts.
                //
                Console.WriteLine("Upserting new user.");

                cluster.CreateManager().UpsertUser(AuthenticationDomain.Local, "jsmith123", "jsmith123pwd", "John Smith",
                    new Role[]
                    {
                        // Roles required for the reading of data from
                        // the bucket.
                        //
                        new Role {Name = "data_reader", BucketName = "couchbasegames"},
                        new Role {Name = "query_select", BucketName = "couchbasegames"},
                        
                        // Roles required for the writing of data into
                        // the bucket.
                        //
                        new Role {Name = "data_writer", BucketName = "couchbasegames"},
                        new Role {Name = "query_insert", BucketName = "couchbasegames"},
                        new Role {Name = "query_delete", BucketName = "couchbasegames"},
                        
                        // Role required for the creation of indexes
                        // on the bucket.
                        //
                        new Role {Name = "query_manage_index", BucketName = "couchbasegames"}
                    }
                );
            }


            using (var cluster = new Cluster(config))
            {
                // Re-access the cluster as the created user.
                //
                Console.WriteLine("User now connecting and authenticating.");
                cluster.Authenticate("jsmith123", "jsmith123pwd");

                Console.WriteLine("Opening couchbasegames bucket as user.");
                var bucket = cluster.OpenBucket("couchbasegames");

                // Perform a N1QL Query to return document IDs from the bucket. These IDs will be
                // used to reference each document in turn, and check for extended attributues
                // corresponding to discounts.
                //
                Console.WriteLine("Searching for discounts, as user.");
                var result = cluster.Query<dynamic>(
                    "SELECT id, meta(`couchbasegames`).id " +
                    "AS docID FROM `couchbasegames`;"
            );

                // Get the docID of each document returned, and use the ID to determine whether
                // the extended attribute exists.
                //
                var resultsReturned = string.Empty;
                var searchPath = "discounts.jsmith123";

                foreach (var row in result)
                {
                    // Get the docID of the current document.
                    //
                    var theId = (string)row.docID;

                    // Determine whether a hotel-discount has been applied to this user.
                    //
                    var whetherDiscountExistsForUser = bucket.LookupIn<dynamic>(theId)
                        .Exists(searchPath, SubdocPathFlags.Xattr)
                        .Execute();

                    // If so, get the discount-percentage.
                    //
                    if (whetherDiscountExistsForUser.Success)
                    {
                        var percentageValueOfDiscount = bucket.LookupIn<dynamic>(theId)
                            .Get(searchPath, SubdocPathFlags.Xattr)
                            .Execute();

                        // If the percentage-value is greater than 15, include the document in the
                        // results to be returned.
                        //
                        if (percentageValueOfDiscount.Content<int>(searchPath) > 15)
                        {
                            resultsReturned = resultsReturned + Environment.NewLine + bucket.Get<dynamic>(theId);
                        }
                    }
                }

                // Display the results, which features only hotels offering more than a 15% discount
                // to the current user.
                //
                Console.WriteLine("Results returned are: {0}", resultsReturned);
            }
        }

    
    


        private static async void MainSync3()
        {
            GetChangesRequest request = new GetChangesRequest();
            request.IncludeDocs = true;
            request.Since = "0";
            request.Heartbeat = 1000;
            request.Feed = ChangesFeed.Continuous;
            request.Style = ChangesStyle.AllDocs;

            DbConnectionInfo connectionInfo = new DbConnectionInfo("http://localhost:4984", "couchbasegames");
            IDbConnection db = new DbConnection(connectionInfo);


            SerializationConfiguration serializationConfiguration = new SerializationConfiguration(new DefaultContractResolver());

            ISerializer serializer = new DefaultSerializer(serializationConfiguration, new DocumentSerializationMetaProvider(), null);

            Changes changes = new Changes(db, serializer);

            try {
                using (var res = changes.GetAsync(request, data =>
                {

                    Console.WriteLine(data);


                }, new CancellationTokenSource().Token))



                    SpinWait.SpinUntil(() => Console.ReadLine() == "C");
            }

            catch (Exception ex) {

                Console.WriteLine(ex.Message);
            }
           
        }

        private static async void MainSync2()
        {



            GetChangesRequest request = new GetChangesRequest();
            request.IncludeDocs = false;
            request.Since = "0";
            request.Heartbeat = 1000;
            request.Feed = ChangesFeed.Continuous;
            request.Style = ChangesStyle.AllDocs;

            DbConnectionInfo connectionInfo = new DbConnectionInfo("http://localhost:4984", "couchbasegames");
            IDbConnection db = new DbConnection(connectionInfo);


            SerializationConfiguration serializationConfiguration = new SerializationConfiguration(new DefaultContractResolver());

            ISerializer serializer = new DefaultSerializer(serializationConfiguration, new DocumentSerializationMetaProvider(), null);

            Changes changes = new Changes(db, serializer);
            

            IObservable<string> observable = changes.ObserveContinuous(request, new CancellationToken(false));

            observable.Subscribe(new MyObserver()
            {

                InterceptOnNext = v =>
                {


                    // Console.WriteLine(" no conflict detected  Id :  change  {0}",  v);
                    if (!string.IsNullOrEmpty(v))
                    {
                        RootObject c = JsonConvert.DeserializeObject<RootObject>(v);

                        if (c.changes.Count > 1)
                        {
                            if (conflicts.ContainsKey(c.id))
                                conflicts[c.id] = c.changes;
                            else
                                conflicts.Add(c.id, c.changes);

                            c.changes.ForEach(x => { Console.WriteLine(" conflict detected : id {0} and changes {1}", c.id, x.rev); });

                        }
                        else
                        {
                            Console.WriteLine(" no conflict detected  Id : {0},  change  {1}", c.id, v);
                        }

                    }
                },

                InterceptOnError =e => {
                    Console.WriteLine(e.Message);
                }

            });

            SpinWait.SpinUntil(() => Console.ReadLine() == "C");
        }





        private static async void MainSync()
        {

            string API_URL = "http://localhost:4984/couchbasegames/_changes?since=1755&heartbeat=1000";

            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                UseProxy = false
            };

            using (var client = new HttpClient(handler, true))
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

                var request = new HttpRequestMessage(HttpMethod.Get, API_URL);


                try
                {
                    using (var response = client.SendAsync(
                    request, HttpCompletionOption.ResponseHeadersRead).Result) 
                    {
                        using (var body = await response.Content.ReadAsStreamAsync())
                        using (var reader = new StreamReader(body))
                            while (!reader.EndOfStream)
                                Console.WriteLine(reader.ReadLine());
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

    }


    public class Change
    {
        public string rev { get; set; }
    }

    public class RootObject
    {
        public int seq { get; set; }
        public string id { get; set; }
        public List<Change> changes { get; set; }
    }

}
