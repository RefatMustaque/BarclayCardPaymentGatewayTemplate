# Sample Code for Barclay Payment Gateway Integration

This is sample code to demonstrate the integration of the Barclaycard payment gateway.

## Barclay Test Account

Before you can run the demo code, you may want to create a [Barclay merchant test account](https://mdepayments.epdq.co.uk/Ncol/Test/BackOffice/accountcreation/create?ISP=epdq&acountry=gb).

- First, activate the account by accepting the terms and conditions on the homepage.
- Then go to **Configuration**:
  - **Technical Information**:
    - **Global Security Parameters**:
      - Select the Hash Algorithm **SHA-256**.
    - **Data and Origin Verification**:
      - Set your desired **SHA-IN passphrase**.
      - Set this passphrase in the application in the `AppConstants` class:
        - `GatewayConfigurations`:
          - `GatewaySHAInSecretKey`
    - **Transaction Feedback**:
      - Select "I would like to receive transaction feedback parameters on the redirection URLs."
      - Select "I would like Barclaycard to display a 'processing' message to the customer during payment processing."
      - Set your desired **SHA-OUT passphrase**.
      - Set this passphrase in the application in the `AppConstants` class:
        - `GatewayConfigurations`:
          - `GatewaySHAOutSecretKey`
  - **Payment Methods**:
    - Add all the necessary brands, e.g., Visa, American Express.
    - Make sure to activate the payment methods.
- Set your test account **PSPID** in the application in the `AppConstants` class:
  - `GatewayConfigurations`:
    - `GatewayPSPID`

## What the Sample Code Demonstrates

- This sample code demonstrates how you can alphabetically order data, convert it into SHA-256, and submit it to the payment gateway.
  - This ensures that data is secured between your site and the payment gateway.
- This sample code also demonstrates how you can receive and validate the data by converting the post-payment transaction feedback with the SHA-OUT passphrase.
  - This ensures that any fraudulent payment is not processed accidentally.
