/////////////////////////////
//Borovikov Nikita (c) 2014//
//Email Checker//////////////
/////////////////////////////

module MailRegex

open System.Text.RegularExpressions


let emailCheck = Regex("^[_a-zA-Z]([.]?(\w)+)*[@]([a-zA-Z]+\.)+([a-zA-Z]{2,4}|com|ru|cc|aero|arpa|asia|biz|cat|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|post|pro|tel|travel|xxx)$").IsMatch