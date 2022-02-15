# Customer Onboarding

A Microservice Api that onboards a customer while exposing the following endpoints:

1. Onboard a customer: The endpoint takes phone Number, email , password, state of residence, and LGA. 

    Business Rule 

    (i) The phone number is verified via OTP before an onboarding is said to be completed 

    (ii) The lga must be mapped to the state selected.

    (iii) Mock sending the OTP.
    

2. Endpoint to return all existing customers previously onboarded 

3. An endpoint that will return existing Bank by consuming endpoint called Getbanks from https://wema-alatdev-apimgt.developer.azure-api.net/api-details#api=alat-tech-test-api

 
