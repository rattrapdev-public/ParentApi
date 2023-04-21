workspace "Security API" "This is an implementation of Security API architecture showing how everything relates" {

    model {

        parent = person "Parent" "Manages Children and buys Toys from the Toy Store"

        catalogManager = person "Catalog Manager" "Manages Children and buys Toys from the Toy Store"

        toyStore = softwareSystem "Parent Toy Store" "Toy store for purchasing toys, managing toy box and catalog." {

            mobileStoreFront = container "Toy Store Mobile App" "App used for purchasing toys online" "Mobile App"

            catalogWebApp = container "Catalog Management Site" "Web site used for managing toy store catalog." "Angular" "Web App"

            familyWebApp = container "Parent Family Management Site" "Site used for managing toy box and family" "Web App"

            parentApi = container "Parent API" "API used for managing family and storing purchased toys within toybox" "Asp.NET Core" {
                guardianController = component "GuardianController" "Manage requests for guardian operations include upsert and retrieval" "C#"
                guardianRepository = component "GuardianRepository" "Repository for managing guardian data" "C#"
                childController = component "ChildController" "Manage requests for children including adding toys" "C#"
                childRepository = component "ChildRepository" "Repository for managing child data" "C#"
            }

            toyStoreApi = container "Toy Store API" "API used for purchasing toys" "Asp.NET Core" {
                purchaseController = component "PurchaseController" "Manage purchase requests made to the toy store" "C#"
                eventPublisher = component "EventPublisher" "Publisher for sending ToyPrchased message." "C#"
                paymentService = component "PaymentService" "Service for processing payment information with the Payment Processor" "C#"
                purchaseRepository = component "PurchaseRepository" "Repository for storing purchase information" "C#"
            }

            toyCatalogApi = container "Toy Catalog API" "API used for managing catalog" "Asp.NET Core" {
                toyCatalogController = component "ToyCatalogController" "Controller used to manage the toy catalog" "C#"
                toyCatalogRepository = component "ToyCatalogRepository" "Repository for storing the ToyCatalog" "C#"
            }

            sqlServer = container "Sql Server Database" "Database used for storing parent, child, toybox, purchases and catalog information" "Sql Server" "Database"

            parentSubscriber = container "Parent Subscriber" "Azure function subscribes to Azure Message Bus and processes messages back to API" "Asp.NET Core"

        }

        # External Service

        shippingSystem = softwareSystem "Shipping System" "Used for shipping and tracking shipments" "External Service"

        paymentProcessor = softwareSystem "Payment Processor" "Used for processing credit card transations" "External Service"

        azureMessageBus = softwareSystem "Azure Message Bus" "Used for receiving messages from apis and publishing them to the subscriber" "External Service"

        # Relations
        parent -> mobileStoreFront "Purchases toys"
        
        parent -> familyWebApp "Manages family"

        catalogManager -> catalogWebApp "Manages the toys available in the toy store"

        mobileStoreFront -> toyCatalogController "HTTP : JSON" "Read toy catalog"

        mobileStoreFront -> purchaseController "HTTP : JSON" "Purchase toy"

        mobileStoreFront -> guardianController "HTTP : JSON" "Get parent information for purchasing"

        mobileStoreFront -> childController "HTTP : JSON" "Get child information for storing to toybox"

        familyWebApp -> guardianController "HTTP : JSON" "Read/write parent information"

        familyWebApp -> childController "HTTP : JSON" "Read/write child information and add toys"

        catalogWebApp -> toyCatalogController "HTTP : JSON" "Read/write toy catalog"

        toyStoreApi -> toyCatalogController "HTTP : JSON" "Read and validate toy purchase"

        toyStoreApi -> shippingSystem "Send toys to children"
        
        paymentService -> paymentProcessor "Processes payments with Processor"
        
        eventPublisher -> azureMessageBus "Send ToyPurchased message"

        purchaseRepository -> sqlServer "Store toy store purchases"

        toyCatalogRepository -> sqlServer "Story toy catalog"

        guardianRepository -> sqlServer "Store parent information"

        childRepository -> sqlServer "Store child and toybox information"

        azureMessageBus -> parentSubscriber "Send messages to subscriber"

        parentSubscriber -> childController "HTTP : JSON" "Post purchased toy information for addition to toy box"

        # Component relations

        childController -> childRepository "Get and store child data"

        guardianController -> guardianRepository "Get and store parent data"

        toyCatalogController -> toyCatalogRepository "Get and store toy catalog"

        purchaseController -> purchaseRepository "Get and store toy purchases"

        purchaseController -> paymentService "Send payment information for processing"

        purchaseController -> eventPublisher "Send toy purchase data to be published"
    }

    views {

        systemContext toyStore "toyStore" {
            autoLayout
            include *
        }

        container toyStore "toyStoreContainer" {
            include *
            autoLayout
        }

        component parentApi "ParentApi" "Parent API Component Diagram" {
            include *
            autoLayout
        }

        component toyCatalogApi "ToyCatalogApi" "Toy Catalog API Component Diagram" {
            include *
            autoLayout
        }

        component toyStoreApi "ToyStoreApi" "Toy Store API Component Diagram" {
            include *
            autoLayout
        }

        theme default

        styles {

            element "Mobile App" {
                shape MobileDeviceLandscape
            }
            
            element "Web App" {
                shape WebBrowser
            }

            element "External Service" {
                shape Component
                background #999999
            }

            element "Database"{
                shape Cylinder
                background #999999
            }

        }

    }

}