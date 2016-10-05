using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Xunit;

namespace CollectionJson.UnitTest
{
    public class Tutorial01
    {
        //http://amundsen.com/media-types/tutorials/collection/tutorial-01.html
        string pathfilter = "/favicon.ico /sortbyemail /sortbyname /filterbyname";
        string cType = "application/vnd.collection+json";
        string expectedFile = "Tutorial01.expected.json";

        [Fact]
        public void AddingFriends()
        {
            var req = WebRequest.Create("http://localhost:1337/");
            var json = Handler(req);
            //http.createServer(handler).listen(port);

            Assert.Equal(json, File.ReadAllText(expectedFile), true, true, true);
        }

        string Handler(WebRequest req)
        //string Handler(HttpRequest req, out HttpResponse res)
        {
            var baseUrl = new Uri("http://" + req.RequestUri.Host + ":" + req.RequestUri.Port);
            var path = req.RequestUri.PathAndQuery;
            if (pathfilter.IndexOf(path) != -1)
            {
                path = "/";
            }

            var friends = GetFriends();
            var cj = CreateCjTemplate(baseUrl, path);
            cj.Collection.Items = RenderItems(friends, baseUrl);
            
            //res.StatusCode = 200;
            //res.Status = "OK";
            //res.ContentType = cType;
            //res.Write(cj.Dump());
            return cj.ToJson();
        }

        private List<Friend> GetFriends()
        {
            Friend item;

            var friends = new List<Friend>();

            item = new Friend();
            item.Name = "mildred";
            item.Email = "mildred@example.com";
            item.Blog = "http://example.com/blogs/mildred";
            friends.Add(item);

            item = new Friend();
            item.Name = "mike";
            item.Email = "mike@example.com";
            item.Blog = "http://example.com/blogs/mike";
            friends.Add(item);

            item = new Friend();
            item.Name = "mary";
            item.Email = "mary@example.com";
            item.Blog = "http://example.com/blogs/mary";
            friends.Add(item);

            item = new Friend();
            item.Name = "mark";
            item.Email = "mark@example.com";
            item.Blog = "http://example.com/blogs/mark";
            friends.Add(item);

            item = new Friend();
            item.Name = "muffin";
            item.Email = "muffin@example.com";
            item.Blog = "http://example.com/blogs/muffin";
            friends.Add(item);

            return friends;
        }

        //render data object (friends) as valid Cj items
        private RecordInfo[] RenderItems(List<Friend> friends, Uri baseUrl)
        {
            var items = friends.Select(friend =>
            {
                var item = new RecordInfo();
                item.Href = new Uri(baseUrl, '/' + friend.Name);
                item.Data = CollectionHelper.Convert(friend, excludes: nameof(friend.Blog));
                item.Links = new LinkInfo[] {
                    new LinkInfo {
                        Rel = "alternate",
                        Href = new Uri(friend.Blog),
                        Prompt = "blog",
                    }
                };
                return item;
            }).ToArray();

            return items;
        }

        CjTemplate CreateCjTemplate(Uri baseUrl, string path)
        {
            var cj = new CjTemplate();
            cj.Collection = new CjCollection();
            cj.Collection.Version = "1.0";
            cj.Collection.Href = new Uri(baseUrl, path);

            //cj.Collection.links = [];
            //cj.Collection.links.push({ 'rel':'home', 'href' : base});
            
            cj.Collection.Queries =  new QueryTemplateInfo[0];
            cj.Collection.Template = null;

            return cj;
        }

        private class Friend
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Blog { get; set; }
        }
    }
}
