#	Application (just for instance)
* **Common**
    * **Commands**
        * _IPartnerCommand.cs_ `(User command interface)`
        * _PartnerCommandHandler.cs_ `(User command handle)`
* **Partner**
    * **Commands**
        * _AcceptPartner.cs_ `(Command request paraméterek)`
        * _AcceptPartnerHandler.cs_ `(Handler folyamatok, Taskok kezelése)`
        * _AcceptPartnerResponseDto.cs_ `(Command Response paraméterek)`
        * _AddressDto.cs_ `(Address külön osztályban)`        
    * **Options**
    * **Queries** `(View model osztályok {get, set} kezelése)`
        * _AddressViewModel.cs_ `(public string State { get; set; })`
        * _PartnerHeaderViewModel.cs_
        * _PartnerDetailsViewModel.cs_
        * _IPartnerQueries.cs_
        * _PartnerQueries.cs_ `(Itt van lekezelve, hogy az adott method hívás milyen Viewmodel-t adjon vissza - A controller hívás esetén össze van kötve a WebAutofacModule-ban a Dependece injection)`
    * **Services**
        * _IPartnerNotificationService.cs_        
    * **Validators**
        * _AddressValidator.cs_ `(Address validáció)`
        * _InvitePartnerValidator.cs_
        * _..._
* _PartnerMappingProfile.cs_ `(Osztályok mappelése automap használatához. CreateMap<AddressDto, Address>();)`
***
## Common `(All of the solution common files)`
* _FunctionCodes.cs_
* _ApiErrorException.cs_
* _ExecutonError.cs_
* _…_
***
## Domain `(Üzleti adatokat és funkciókat tartalmazó osztáyok, ez a legalsó réteg)`
* **PartnerAggregate**
    * **Specification**
        * _CompanyNameSpecification.cs_ `()`
        * _ExistsByPartnerGuidSpecification.cs_
        * _ExistsByPartnerIdSpecification.cs_
        * _InvitedEmailSpecification.cs_
        * _PartnerEmailSpecification.cs_ `(Itt specifikálunk egy alap szűrő feltételt, amelyet máshol tudunk használni)`
        * _PartnerStatusSpecification.cs_
    * _Partner.cs_ `(Itt vannak azok az alap műveletek, amelyeket máshol tudunk használni)`
    * _PartnerId.cs_
    * _PartnerStatus.cs_
***
## Infrastructure `(External service files)`
* **Common**
    * **EmailService**
        * _EmailOptions.cs_
        * _EmailService.cs_
    * **EmailTemplateProvider**
        * _EmailContent.cs_
        * _EmailTemplateProvider.cs_
        * _IEmailTemplateProvider.cs_
        * _…_
* **User**
    * **Models**
        * **Db**
            * _User.cs_
            * _UserDbContext.cs_
        * **Noitifications**
            * _UserInviteConfirmationViewModel.cs_
        * _DefaultIdentityConfiguration.cs_
    * _IdentityUserService.cs_
    * _SmartCityUserService.cs_
    * _UserEmailNotificationService.cs_
* _InfrastructureAutofacModule.cs_
***
## WebApi 
* **Attributes**
    * _ValidateModelAttribute.cs_ `()`
* **Controllers**
    * _ApiControllerBase.cs_ `(Base class for every controller)`
    * _Extensions.cs_ `(???)`
    * _UserController.cs_ `(Action methods in WEB Api [HttpGet, HttpPost])`
* **Models**
    * **Api**
        * _PartnerStatusResponse.cs_
        * _UserStatusResponse.cs_
* **Swagger** `(Ezek alapján tölti ki a swagger-t példa adatokkal, pl.Post hívásnál)`
    * _InvitePartnerSwaggerExmaple_
    * _…_
* **Views**
* _appsettings.json_
* _Permissions.cs_ `(Jogosultságok kezelése, milyen permission szükséges az adott action hívásához)`
* _Program.cs_ `(A Service itt indul el)`
* _Startup.cs_ `(A Service során használt configok itt találhatóak, pl.: DB connections)`
* _WebAutofacModule.cs_ `(Dependency injection config, avagy ha Interface-t kérek, melyik implementációt szeretném megkapni. builder.RegisterType<HttpExceptionProvider>().As<IHttpExceptionProvider>().SingleInstance();)`

