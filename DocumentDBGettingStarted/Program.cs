using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DocumentDBGettingStarted
{
    class Program
    {
        private static readonly string databaseId = "membership";
        private const string endpointUrl = "https://craft135.documents.azure.com:443/";
        private const string authorizationKey = "GoI177W6s9VTChkpk4lzk5FEWLXNJXWi6Sb2kN1N1wyc8o4ba1gNEMc8ARqyQaZYrZ80vDvcKzk1E9sbwmlpJg==";
        private static DocumentClient client;
        private static async Task ViewDatabases(DocumentClient client)
        {
            Console.WriteLine();
            Console.WriteLine(">>> View Databases <<<");
            var databases = client.CreateDatabaseQuery().ToList();

            List<Member> members = new List<Member>();
     
            try
            {
                var database = databases.Where(d => d.Id == "membership").FirstOrDefault();
                if (database != null)
                {
                    DocumentCollection collection = client.CreateDocumentCollectionQuery(database.CollectionsLink, "SELECT * FROM c WHERE c.id ='craftmembership'").AsEnumerable().FirstOrDefault();
                    if (collection != null)
                    {

                        var documents = client.CreateDocumentQuery(collection.SelfLink, "SELECT * FROM c WHERE c.FirstName = 'Michael'").ToList();
                     
                        


                        foreach (Document document in documents)
                        {
                            Member member = JsonConvert.DeserializeObject<Member>(document.ToString());
                            members.Add(member);
                            Console.WriteLine(document.ToString());

                        }
                        Console.Read();
                    }

                }
            }catch(Exception e) { Console.WriteLine(e.Message); Console.Read();  }

            //foreach(var database in databases.Where(d=> d.Id == "membership"))
            //{
            //    Console.WriteLine(String.Format(" Database Id: {0}; Rid: {1}", database.Id, database.ResourceId));
            //    ViewCollections(client, database);

            //}

            //Console.WriteLine();
            //Console.WriteLine(String.Format("Total databases: {0}", databases.Count));
            //Console.Read();
        }
        
        private static void ViewCollections(DocumentClient client,Database database)
        {
            Console.WriteLine();
            Console.WriteLine(">>> View Collections in {0} <<<", database.Id);

            var collections = client.CreateDocumentCollectionQuery(database.CollectionsLink).ToList();

            var collectionCount = 0;

            foreach(var collection in collections)
            {
                collectionCount++;

                Console.WriteLine();
                Console.WriteLine("Collection #{0}", collectionCount);
                ViewCollection(collection);

                Console.WriteLine();
                Console.WriteLine("Total collections in database {0} : {1}",database.Id,collections.Count);

            }
        }
        private static void ViewCollection(DocumentCollection collection)
        {
            Console.WriteLine(" Collection ID: {0}", collection.Id);
        }
        static void Main(string[] args)
        {
            Member member = new Member()
            {
                FirstName = "Cedric",
                LastName = "McGee",
                Active = true,
                Square = true,
                Demitted = false,
                DOB = new DateTime(1969, 08, 30),
                Position = "Recorder",
                Contact = new ContactInfo()
                {
                    Addresses = new List<Address>()
                      {
                           new Address
                           {
                               City = "Little Elm",
                               State = "TX",
                                Street = "1113 SnowBird Dr.",
                                 Zip = "75068"
                           },
                      },
                    Emails = new List<Email>()
                       {
                           new Email
                           {
                                Address = "cedric.mcgee2@att.net"
                           }
                        },
                    Phones = new List<Phone>()
                        {
                            new Phone
                            {
                                Number = "817.304.4328",
                                 PhoneNumberType = NumberType.Cell
                            }
                        },

                },
                EmergencyContact = new EmergencyContact()
                {
                     FirstName = "Kimberly",
                     LastName = "McGee",
                     Contact = new ContactInfo()
                     {
                         Addresses = new List<Address>()
                      {
                           new Address
                           {
                               City = "Little Elm",
                               State = "TX",
                                Street = "1113 SnowBird Dr.",
                                 Zip = "75068"
                           },
                      },
                         Emails = new List<Email>()
                       {
                           new Email
                           {
                                Address = "mcgee_kimberly@yahoo.com"
                           }
                        },
                         Phones = new List<Phone>()
                        {
                            new Phone
                            {
                                Number = "972.639.8326",
                                 PhoneNumberType = NumberType.Cell
                            }
                        },
                     }
                }
            };


            string memberJson = JsonConvert.SerializeObject(member);

            Console.Read();

            //string something = String.Empty;
            //using (var client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            //{
            //   ViewDatabases(client);
            //}
             // IQueryable<Member> memberQuery = documentClient.CreateDocumentQuery<Member>(UriFactory.CreateDatabaseUri)
           
            //IEnumerable<Database> databases = documentClient.CreateDatabaseQuery().AsEnumerable<Database>();
           
            //foreach(var database in databases)
            //{
            //    Console.WriteLine(database.Id);
            //}



            //try
            //{
            //    using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            //    {
            //        RunDatabaseDemo().Wait();
            //    }
            //    //  Program p = new Program();
            //    // p.GetstartDemo().Wait();
                
            //    // p.ExecuteSimpleQueryAsync("membership", "craftmembership");

            //}
            //catch (DocumentClientException de)
            //{
            //    Exception baseException = de.GetBaseException();
            //    Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            //}
            //catch (Exception e)
            //{
            //    Exception baseException = e.GetBaseException();
            //    Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            //}
            //finally
            //{
            //   //  Console.WriteLine("End of demo, press any key to exit.");
            //   //  Console.ReadKey();
            //}

            //try
            //{
            //    Program p = new Program();
            //    p.GetStartedDemo().Wait();

            //}
            //catch(DocumentClientException de)
            //{
            //    Exception baseException = de.GetBaseException();
            //    Console.WriteLine("{0} error occured : {1}, Message : {2}", de.StatusCode, de.Message, baseException.Message);

            //}
            //catch(Exception e)
            //{
            //    Exception baseException = e.GetBaseException();
            //    Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            //}
            //finally
            //{
            //    Console.WriteLine("End of demo, press any key to exit.");
            //    Console.Read();
            //}
        }
        private static async Task RunDatabaseDemo()
        {
            //********************************************************************************************************
            // 1 -  Query for a Database
            //
            // Note: we are using query here instead of ReadDatabaseAsync because we're checking if something exists
            //       the ReadDatabaseAsync method expects the resource to be there, if its not we will get an error
            //       instead of an empty 
            //********************************************************************************************************
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == databaseId).AsEnumerable().FirstOrDefault();
            Console.WriteLine("1. Query for a database returned: {0}", database == null ? "no results" : database.Id);

            //check if a database was returned
            if (database == null)
            {
                //**************************
                // 2 -  Create a Database
                //**************************
                database = await client.CreateDatabaseAsync(new Database { Id = databaseId });
                Console.WriteLine("\n2. Created Database: id - {0} and selfLink - {1}", database.Id, database.SelfLink);
            }

            //*********************************************************************************
            // 3 - Get a single database
            // Note: that we don't need to use the SelfLink of a Database anymore
            //       the links for a resource are now comprised of their Id properties
            //       using UriFactory will give you the correct URI for a resource
            //
            //       SelfLink will still work if you're already using this
            //********************************************************************************
            database = await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId));
            Console.WriteLine("\n3. Read a database resource: {0}", database);

            //***************************************
            // 4 - List all databases for an account
            //***************************************
            var databases = await client.ReadDatabaseFeedAsync();
            Console.WriteLine("\n4. Reading all databases resources for an account");
            foreach (var db in databases)
            {
                Console.WriteLine(db);
            }

            //*************************************
            // 5 - Delete a Database using its Id
            //*************************************
            await client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId));
            Console.WriteLine("\n5. Database {0} deleted.", database.Id);
        }
        //private static async Task RunDatabaseDemo1()
        //{
        //    DocumentClient documentClient = new DocumentClient(new Uri(EndPointUri), PrimaryKey);
        //    Database database = await documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri("membership"));
            
        //    Console.Read();
        //}
        private async Task GetstartDemo()
        {
            // this.client = new DocumentClient(new Uri(EndPointUri), PrimaryKey);
        }
        private  void ExecuteSimpleQueryAsync(string databasename, string collectionname)
        {

            //DocumentClient documentClient = new DocumentClient(new Uri(endpointUrl), PrimaryKey);
            //var response = await documentClient.ReadDocumentAsync(UriFactory.CreateDocumentUri("membership", "craftmembership", ""));
            //IQueryable<Member> members = documentClient.CreateDocumentQuery<Member>(UriFactory.CreateDocumentCollectionUri("membership", "craftmembership")); // .Where(x => x.LastName == "Anderson");

            //List<Member> membersFound = members.ToList();

            //DocumentClient documentClient = new DocumentClient(new Uri(EndPointUri), PrimaryKey);
            //DocumentCollection collection = await documentClient.ReadDocumentCollectionAsync(String.Format("/dbs/{0}/colls/{1}",databasename,collectionname));
            //// Set some common query options 
            //FeedOptions feedOptions = new FeedOptions { MaxItemCount = -1 };
            //IQueryable<Member> memberQuery = this.client.CreateDocumentQuery<Member>(UriFactory.CreateDocumentCollectionUri(databasename, collectionname), feedOptions);
            //                                                                         // .Where(c => c.LastName == "Anderson");

            //foreach(var member in memberQuery)
            //{
            //    Console.WriteLine(member.FirstName);
            //}

            Console.Read();
        }
        private async Task GetStartedDemo()
        {
           //  this.client = new DocumentClient(new Uri(EndPointUri), PrimaryKey);
        }
    }
}
