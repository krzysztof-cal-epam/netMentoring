ï
nC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Interfaces\IRepository.cs
	namespace 	
CatalogService
 
. 
Domain 
.  

Interfaces  *
{ 
public 

	interface 
IRepository  
<  !
T! "
>" #
where$ )
T* +
:, -
class. 3
{ 
Task 
< 
T 
> 
GetByIdAsync 
( 
int  
id! #
)# $
;$ %
Task 
< 
IEnumerable 
< 
T 
> 
> 
	ListAsync &
(& '
)' (
;( )
Task 
< 
T 
> 
AddAsync 
( 
T 
entity !
)! "
;" #
Task 
UpdateAsync 
( 
T 
entity !
)! "
;" #
Task		 
DeleteAsync		 
(		 
int		 
id		 
)		  
;		  !
Task

 
AddOutboxEventAsync

  
(

  !
string

! '
	eventType

( 1
,

1 2
object

3 9
payload

: A
)

A B
;

B C
} 
} µ
uC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Interfaces\IProductRepository.cs
	namespace 	
CatalogService
 
. 
Domain 
.  

Interfaces  *
{ 
public 

	interface 
IProductRepository '
:( )
IRepository* 5
<5 6
Product6 =
>= >
{ 
Task 
< 
IEnumerable 
< 
Product  
>  !
>! "
	ListAsync# ,
(, -
int- 0
?0 1

categoryId2 <
,< =
int> A
pageB F
,F G
intH K
pageSizeL T
)T U
;U V
Task (
UpdateProductWithOutboxAsync )
() *
Product* 1
product2 9
,9 :
string; A
	eventTypeB K
,K L
objectM S
payloadT [
)[ \
;\ ]
}

 
} ﬁ
vC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Interfaces\ICategoryRepository.cs
	namespace 	
CatalogService
 
. 
Domain 
.  

Interfaces  *
{ 
public 

	interface 
ICategoryRepository (
:) *
IRepository+ 6
<6 7
Category7 ?
>? @
{ 
Task #
DeleteWithProductsAsync $
($ %
int% (
parentId) 1
)1 2
;2 3
} 
}		 Ú
rC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Interfaces\ICartRepository.cs
	namespace 	
CatalogService
 
. 
Domain 
.  

Interfaces  *
{ 
public 

	interface 
ICartRepository $
{ 
Cart 
GetCartById 
( 
Guid 
cartId $
)$ %
;% &
void 
SaveCart 
( 
Cart 
cart 
)  
;  !
void		 

DeleteCart		 
(		 
Guid		 
cartId		 #
)		# $
;		$ %
IEnumerable

 
<

 
Cart

 
>

 
GetAllCarts

 %
(

% &
)

& '
;

' (
} 
} ¸
pC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Interfaces\ICartDatabase.cs
	namespace 	
CatalogService
 
. 
Domain 
.  

Interfaces  *
{ 
public 

	interface 
ICartDatabase "
<" #
T# $
>$ %
{ 
T 	
FindById
 
( 
Guid 
id 
) 
; 
void 
Upsert 
( 
T 
item 
) 
; 
void 
Delete 
( 
Guid 
id 
) 
; 
IEnumerable 
< 
T 
> 
GetAll 
( 
) 
;  
}		 
}

 ˝
hC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Entities\Product.cs
	namespace 	
CatalogService
 
. 
Domain 
.  
Entities  (
{ 
public 

class 
Product 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[		 	
Required			 
]		 
[

 	
	MaxLength

	 
(

 
$num

 
)

 
]

 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
=) *
string+ 1
.1 2
Empty2 7
;7 8
public 
string 
? 
Description "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
Uri 
? 
Image 
{ 
get 
;  
set! $
;$ %
}& '
[ 	
Required	 
] 
public 
decimal 
Price 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Required	 
] 
public 
uint 
Amount 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Required	 
] 
public 
int 

CategoryId 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
] 
public 
Category 
Category  
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
} ’
wC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Exceptions\RepositoryExceptions.cs
	namespace 	
CatalogService
 
. 
Domain 
.  

Exceptions  *
{ 
public 

class !
CartNotFoundException &
:' (
	Exception) 2
{ 
public !
CartNotFoundException $
($ %
Guid% )
cartId* 0
)0 1
:2 3
base4 8
(8 9
$"9 ;
$str; M
{M N
cartIdN T
}T U
$strU d
"d e
)e f
{g h
}i j
} 
public 

class !
ItemNotFoundException &
:' (
	Exception) 2
{		 
public

 !
ItemNotFoundException

 $
(

$ %
int

% (
itemId

) /
,

/ 0
Guid

1 5
cartId

6 <
)

< =
:

> ?
base

@ D
(

D E
$"

E G
$str

G Y
{

Y Z
itemId

Z `
}

` a
$str	

a Ç
{


Ç É
cartId


É â
}


â ä
$str


ä ã
"


ã å
)


å ç
{


é è
}


ê ë
} 
public 

class 
RepositoryException $
:% &
	Exception' 0
{ 
public 
RepositoryException "
(" #
string# )
message* 1
,1 2
	Exception3 <
innerException= K
=L M
nullN R
)R S
:T U
baseV Z
(Z [
message[ b
,b c
innerExceptiond r
)r s
{t u
}v w
} 
} ¶

uC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Exceptions\DatabaseExceptions.cs
	namespace 	
CatalogService
 
. 
Domain 
.  

Exceptions  *
{ 
public 

class !
DatabaseReadException &
:' (
	Exception) 2
{ 
public !
DatabaseReadException $
($ %
string% +
message, 3
,3 4
	Exception5 >
innerException? M
=N O
nullP T
)T U
:V W
baseX \
(\ ]
message] d
,d e
innerExceptionf t
)t u
{v w
}x y
} 
public 

class "
DatabaseWriteException '
:( )
	Exception* 3
{		 
public

 "
DatabaseWriteException

 %
(

% &
string

& ,
message

- 4
,

4 5
	Exception

6 ?
innerException

@ N
=

O P
null

Q U
)

U V
:

W X
base

Y ]
(

] ^
message

^ e
,

e f
innerException

g u
)

u v
{

w x
}

y z
} 
} å

gC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Entities\Outbox.cs
	namespace 	
CatalogService
 
. 
Domain 
.  
Entities  (
{ 
public 

class 
Outbox 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
	EventType 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Payload 
{ 
get  #
;# $
set% (
;( )
}* +
public		 
bool		 
IsProcessed		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
DateTime

 
	CreatedAt

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
public 
DateTime 
? 
ProcessedAt $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} °
jC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Entities\ImageItem.cs
	namespace 	
CatalogService
 
. 
Domain 
.  
Entities  (
{ 
public 

class 
	ImageItem 
{ 
public 
string 
ImageUrl 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
? 
AltText 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
}		 ¸

iC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Entities\Category.cs
	namespace 	
CatalogService
 
. 
Domain 
.  
Entities  (
{ 
public 

class 
Category 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[		 	
Required			 
]		 
[

 	
	MaxLength

	 
(

 
$num

 
)

 
]

 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
=) *
string+ 1
.1 2
Empty2 7
;7 8
public 
Uri 
? 
ImageUrl 
{ 
get "
;" #
set$ '
;' (
}) *
public 
int 
? 
ParentCategoryId $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
Category 
? 
ParentCategory '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} 
} „
iC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Entities\CartItem.cs
	namespace 	
CatalogService
 
. 
Domain 
.  
Entities  (
{ 
public 

class 
CartItem 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public		 
	ImageItem		 
?		 
Image		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public 
decimal 
Price 
{ 
get "
;" #
set$ '
;' (
}) *
public 
uint 
Quantity 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} ö
eC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\CatalogService.Domain\Entities\Cart.cs
	namespace 	
CatalogService
 
. 
Domain 
.  
Entities  (
{ 
public 

class 
Cart 
{ 
public 
Cart 
( 
Guid 
id 
) 
{ 	
Id 
= 
id 
; 
} 	
public

 
Guid

 
Id

 
{

 
get

 
;

 
private

 %
set

& )
;

) *
}

+ ,
public 
List 
< 
CartItem 
> 
Items #
{$ %
get& )
;) *
set+ .
;. /
}0 1
=2 3
new4 7
List8 <
<< =
CartItem= E
>E F
(F G
)G H
;H I
} 
} 