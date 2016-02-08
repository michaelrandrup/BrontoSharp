# BrontoSharp
C# based SDK for the Bronto SOAP API, created and maintained independently from the Bronto Professional Services team. Questions for the API may be asked in the official Dev Q&amp;A Forum (http://dev.bronto.com/questions) or see the official documentation at http://dev.bronto.com/api/v4

## Getting started
You will need access to your API Token in your Bronto account

```C#
            // 1. Add your API token below (go to Home > Settings > Data Exchange in Bronto to see your API Key
            string apiToken = "<my API key>";

            // 2. Create your login session by logging in
            Bronto.API.LoginSession login = Bronto.API.LoginSession.Create(apiToken);

            // 3. Reference the Contacts service and add a contact to Bronto
            Contacts contactsApi = new Contacts(login);

            BrontoService.contactObject contact = new BrontoService.contactObject()
            {
                email = "mra@applications.dk",
                status = Bronto.API.ContactStatus.Onboarding
            };

            BrontoResult result = contactsApi.Add(contact);
            if (result.HasErrors)
            {
                result.ErrorIndicies.ToList().ForEach(id =>
                {
                    Console.WriteLine("Error code {0}: {1}", result.Items[id].ErrorCode, result.Items[id].ErrorString);
                });
            }
            else
            {
                Console.WriteLine("A contact with the Id '{0}' was created", result.Items.First().Id);
            }
```
