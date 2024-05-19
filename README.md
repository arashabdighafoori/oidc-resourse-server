<h1 align="center">
   <b>
        oidc-resourse-server
   </b>
</h1>

<p align="center">
An OpenID Connect resourse server built with .Net Core 7 minimal api.
</p>

<br />

<!-- <div align="center"></div> -->

<br />

## Table of Contents

- [Features](#features)
- [What's missing?](#what-is-missing)

<br />

## Features

- Fingerprinting (clientjs)
- Bot detection (@fingerprintjs/botd)
- Client-Side Internationalization
- Client-Side and Server-Side Feature Flags
- Themes

<br />

## What is missing?

- Multifactor authentication is not implemented so different methods can be used (SMS, Email, Passkey, etc...)
- Only english is added to client languages.
- Fingerprint validtion - if unique clients are not neccessery - can be turned off from `appsettings.json > Features > FingerPrintValidation`.
- Client's feature flags are currently just a object returned from `/api/v1/features` route ( it can be changed to use feature flags from `Microsoft.FeatureManagement` or from `IConfiguration` ).

<br />

## Happy Programming :tada: !

<!--
## License
-->

<!-- [MIT](LICENSE)  -->
