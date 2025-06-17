ú
|C:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Infrastructure\Interfaces\IRabbitMqProducer.cs
	namespace 	
CatalogService
 
. 
Infrastructure '
.' (

Interfaces( 2
{ 
public 

	interface 
IRabbitMqProducer &
:' (
IDisposable) 4
{ 
Task 
PublishAsync 
( 
string  
message! (
,( )
CancellationToken* ;
cancellationToken< M
)M N
;N O
} 
} Ì
sC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Infrastructure\DependencyInjection.cs
	namespace 	
CatalogService
 
. 
Infrastructure '
{ 
public 

static 
class 
DependencyInjection +
{ 
public 
static 
IServiceCollection (
AddInfrastructure) :
(: ;
this 
IServiceCollection #
services$ ,
,, -
string 
connectionString #
,# $
string  
cartConnectionString '
)' (
{ 	
services 
. 
AddDbContext !
<! "
CatalogDbContext" 2
>2 3
(3 4
options4 ;
=>< >
options 
. 
UseSqlServer $
($ %
connectionString% 5
)5 6
)6 7
;7 8
services 
. 
	AddScoped 
( 
typeof %
(% &
IRepository& 1
<1 2
>2 3
)3 4
,4 5
typeof6 <
(< =
GenericRepository= N
<N O
>O P
)P Q
)Q R
;R S
services 
. 
	AddScoped 
( 
typeof %
(% &
IProductRepository& 8
)8 9
,9 :
typeof; A
(A B
ProductRepositoryB S
)S T
)T U
;U V
services 
. 
	AddScoped 
( 
typeof %
(% &
ICategoryRepository& 9
)9 :
,: ;
typeof< B
(B C
CategoryRepositoryC U
)U V
)V W
;W X
services 
. 
	AddScoped 
< 
ICategoryService /
,/ 0
CategoryService1 @
>@ A
(A B
)B C
;C D
services 
. 
	AddScoped 
< 
IProductService .
,. /
ProductService0 >
>> ?
(? @
)@ A
;A B
services 
. 
	AddScoped 
< 
CatalogService -
.- .
Application. 9
.9 :

Interfaces: D
.D E
ICartServiceE Q
,Q R
CatalogServiceS a
.a b
Applicationb m
.m n
Servicesn v
.v w
CartService	w ‚
>
‚ ƒ
(
ƒ „
)
„ …
;
… †
services!! 
.!! 
	AddScoped!! 
<!! 

DataAccess!! )
.!!) *

Interfaces!!* 4
.!!4 5
ICartService!!5 A
,!!A B

DataAccess!!C M
.!!M N
Services!!N V
.!!V W
CartService!!W b
>!!b c
(!!c d
)!!d e
;!!e f
services"" 
."" 
	AddScoped"" 
<"" 
ICartRepository"" .
,"". /
CartRepository""0 >
>""> ?
(""? @
)""@ A
;""A B
services## 
.## 
AddSingleton## !
<##! "
ICartDatabase##" /
<##/ 0
Cart##0 4
>##4 5
>##5 6
(##6 7
provider##7 ?
=>##@ B
{$$ 
return%% 
new%% 
LiteDbCartDatabase%% -
(%%- . 
cartConnectionString%%. B
)%%B C
;%%C D
}&& 
)&& 
;&& 
services(( 
.(( 
AddHostedService(( %
<((% &
OutboxProcessor((& 5
>((5 6
(((6 7
)((7 8
;((8 9
return** 
services** 
;** 
}++ 	
},, 
}-- 