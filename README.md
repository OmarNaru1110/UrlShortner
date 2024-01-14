# Url Shortner
simple asp.net core mvc url shortner web app

## Why?
this is my second project after learning asp.net core mvc,
so I wanted to do something depending on myself for the for the 
first time without watching tutorials step by step, and I didn't 
want to start with large projects without doing some simple projects first.

## Implementation

The whole idea of this app is based on Bijection

1. Get the url from the user
2. Encode it 
3. Return to the user the new encoded url
   
when the user requests the encoded url do the following:
1. Get the original url from the database
2. Redirect the user to it 

## Useful Links
- https://stackoverflow.com/questions/742013/how-do-i-create-a-url-shortener
