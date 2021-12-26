using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace PromoSeeker
{
    internal class SendTweet
    {
        public static async Task Send(Tweet tweetToSend)
        {
            TweetinviEvents.BeforeExecutingRequest += (sender, args) =>
            {
            };

          

            var client = new TwitterClient(credentials);

            TweetinviEvents.SubscribeToClientEvents(client);

            var authenticatedUser = await client.Users.GetAuthenticatedUserAsync();

            //send tweet

            var tweet = await client.Tweets.PublishTweetAsync(tweetToSend.Name + " " + tweetToSend.Url);
            var publishedTweet = await client.Tweets.GetTweetAsync(tweet.Id);
        }
    }
}
