# POPconPasswd
This is a tool to decrypt passwords from popcon that are stored encrypted in the registry
The tool is capable to decrypt passwords that are 52 characters long.  
  
##Why POPconPasswd?##
Well a customer of mine is using popcon but the previous technicians haven't documented the
passwords that are used.  
So I asked myself if these passwords are stored somewhere.  
While looking after them i found them in the registry.  
This passwords normaly are encrypted.  
But with a simple substitution cypher. 
  
##How does POPconPasswd work?##
You can input an encrypted POPcon password an it will show you the decrypted password.  
You can also the feature that reads the pop3 subkeys in the registry that are belonging to popcon.  
In this case all decrypted passwords are shown in an datagrid.  

###So you wanna know how it really works?###
Passwords that are encrypted by popcon use, as mentioned a substitution cypher.  
This cypher encrypts als characters of a password that are alphanumeric.  
All other characters aren't encrypted.  
If the password contains characters like Ü and so on the password is stored unencrypted.  
The alphabet that is used for the encryption/decryption contains 62 characters.  
This alphabet starts with 0-9 followed by A-Z and last but not least a-z.  
The key that is used for the substitution isn't one single key like for caesar cyphers.  
To get this key/keys I used known plaintexts an compared them to the encrypted values.  
After that I came up with the following 52 character long key:  
22,4,18,6,9,18,12,6,4,12,18,6,22,9,12,18,4,9,6,12,18,9,6,12,4,18,9,6,12,6,16,18,22,6,18,9,6,12,18,22,6,9,12,18,22,6,9,12,18,6,22,9  
  
To encrypt a password the first character of a password is substractet by the first charater of the key  
relative to the value of this character in the cypher alphabet.    
Example:
A - 22 = p  

To decrypt a password the first character of a password is added by the first charater of the key  
relative to the value of this character in the cypher alphabet.  
Example:  
p + 22 = A  

But there is something that I'm probably missing.  
If a password is en- or decrypted and the offset value is under the begining or abobe the end  
of the alphabet I have to add 2 for encryption and subtract 2 for decryption.  

But as far I can tell it is working for me so far.  



