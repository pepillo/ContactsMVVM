# Contacts MVVM App

### Installation
Clone project and run Package Manager (restore packages).

```sh
$ git clone https://github.com/pepillo/Contacts.git
```

### Update Connection String
Update connection string in App.config

```
<connectionStrings>
    <add name="WpfEntities" 
         connectionString="metadata=res://*/Data.ContactModel.csdl|res://*
                          /Data.ContactModel.ssdl|res://*/Data.ContactModel.msl;
         provider=System.Data.SqlClient;
         provider connection string=&quot;
         data source=DESKTOP-9JCFSMJ\SQLEXPRESS;
         initial catalog=WpfContacts;
         integrated security=True;
         MultipleActiveResultSets=True;
         App=EntityFramework&quot;"  
         providerName="System.Data.EntityClient" 
    />
</connectionStrings>
```

### Reference

| Reference | Location |
| ------ | ------ |
| Database Structure | Contacts/Data/TableSQL.sql |
