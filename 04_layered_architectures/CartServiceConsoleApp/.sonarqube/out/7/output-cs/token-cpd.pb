Ö
iC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\RestApi\Swagger\ConfigureSwaggerOptions.cs
public

 
class

 #
ConfigureSwaggerOptions

 $
:

% &"
IConfigureNamedOptions

' =
<

= >
SwaggerGenOptions

> O
>

O P
{ 
private 
readonly *
IApiVersionDescriptionProvider 3
	_provider4 =
;= >
public 
#
ConfigureSwaggerOptions "
(" #*
IApiVersionDescriptionProvider# A
providerB J
)J K
{ 
	_provider 
= 
provider 
; 
} 
public 

void 
	Configure 
( 
SwaggerGenOptions +
options, 3
)3 4
{ 
foreach 
( 
var 
description  
in! #
	_provider$ -
.- ."
ApiVersionDescriptions. D
)D E
{ 	
options 
. 

SwaggerDoc 
( 
description 
. 
	GroupName %
,% &
CreateVersionInfo !
(! "
description" -
)- .
). /
;/ 0
} 	
} 
public 

void 
	Configure 
( 
string  
name! %
,% &
SwaggerGenOptions' 8
options9 @
)@ A
{ 
	Configure 
( 
options 
) 
; 
}   
private"" 
OpenApiInfo"" 
CreateVersionInfo"" )
("") *!
ApiVersionDescription""* ?
desc""@ D
)""D E
{## 
var$$ 
info$$ 
=$$ 
new$$ 
OpenApiInfo$$ "
($$" #
)$$# $
{%% 	
Title&& 
=&& 
$str&& =
,&&= >
Version'' 
='' 
desc'' 
.'' 

ApiVersion'' %
.''% &
ToString''& .
(''. /
)''/ 0
}(( 	
;((	 

return** 
info** 
;** 
}++ 
},, ·9
QC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\RestApi\Program.cs
public 
partial 
class 
Program 
{ 
public 

static 
void 
Main 
( 
string "
[" #
]# $
args% )
)) *
{ 
var 
builder 
= 
WebApplication $
.$ %
CreateBuilder% 2
(2 3
args3 7
)7 8
;8 9
builder 
. 
Services 
. 
AddApiVersioning )
() *
options* 1
=>2 4
{ 	
options 
. 
DefaultApiVersion %
=& '
new( +

ApiVersion, 6
(6 7
$num7 8
,8 9
$num: ;
); <
;< =
options 
. /
#AssumeDefaultVersionWhenUnspecified 7
=8 9
true: >
;> ?
options 
. 
ReportApiVersions %
=& '
true( ,
;, -
options 
. 
ApiVersionReader $
=% &
ApiVersionReader' 7
.7 8
Combine8 ?
(? @
new@ C&
UrlSegmentApiVersionReaderD ^
(^ _
)_ `
,` a
new> A"
HeaderApiVersionReaderB X
(X Y
$strY h
)h i
,i j
new> A%
MediaTypeApiVersionReaderB [
([ \
$str\ k
)k l
)l m
;m n
} 	
)	 

;
 
builder 
. 
Services 
. #
AddVersionedApiExplorer 0
(0 1
options1 8
=>9 ;
{ 	
options 
. 
GroupNameFormat #
=$ %
$str& .
;. /
options 
. %
SubstituteApiVersionInUrl -
=. /
true0 4
;4 5
}   	
)  	 

;  
 
var## 
connectionString## 
=## 
builder## &
.##& '
Configuration##' 4
.##4 5
GetConnectionString##5 H
(##H I
$str##I \
)##\ ]
;##] ^
var$$ 
cartConnection$$ 
=$$ 
builder$$ $
.$$$ %
Configuration$$% 2
.$$2 3
GetConnectionString$$3 F
($$F G
$str$$G U
)$$U V
;$$V W
builder&& 
.&& 
Services&& 
.&& 
AddInfrastructure&& *
(&&* +
connectionString&&+ ;
,&&; <
cartConnection&&= K
)&&K L
;&&L M
builder'' 
.'' 
Services'' 
.'' 
AddTransient'' %
<''% &
IConfigureOptions''& 7
<''7 8
SwaggerGenOptions''8 I
>''I J
,''J K#
ConfigureSwaggerOptions''L c
>''c d
(''d e
)''e f
;''f g
builder** 
.** 
Services** 
.** 
	Configure** "
<**" #
RabbitMqSettings**# 3
>**3 4
(**4 5
builder**5 <
.**< =
Configuration**= J
.**J K

GetSection**K U
(**U V
$str**V `
)**` a
)**a b
;**b c
builder++ 
.++ 
Services++ 
.++ 
AddHostedService++ )
<++) *
CartMessageListener++* =
>++= >
(++> ?
)++? @
;++@ A
builder-- 
.-- 
Services-- 
.-- 
AddControllers-- '
(--' (
)--( )
;--) *
builder00 
.00 
Services00 
.00 #
AddEndpointsApiExplorer00 0
(000 1
)001 2
;002 3
builder11 
.11 
Services11 
.11 
AddSwaggerGen11 &
(11& '
)11' (
;11( )
builder33 
.33 
Services33 
.33 
AddSwaggerGen33 &
(33& '
c33' (
=>33) +
{44 	
var66 
xmlFilename66 
=66 
$"66  
{66  !
Assembly66! )
.66) * 
GetExecutingAssembly66* >
(66> ?
)66? @
.66@ A
GetName66A H
(66H I
)66I J
.66J K
Name66K O
}66O P
$str66P T
"66T U
;66U V
c77 
.77 
IncludeXmlComments77  
(77  !
Path77! %
.77% &
Combine77& -
(77- .

AppContext77. 8
.778 9
BaseDirectory779 F
,77F G
xmlFilename77H S
)77S T
)77T U
;77U V
}88 	
)88	 

;88
 
var:: 
app:: 
=:: 
builder:: 
.:: 
Build:: 
(::  
)::  !
;::! "
app<< 
.<< 
UseMiddleware<< 
<<< -
!GlobalExceptionHandlingMiddleware<< ;
><<; <
(<<< =
)<<= >
;<<> ?
if?? 

(?? 
app?? 
.?? 
Environment?? 
.?? 
IsDevelopment?? )
(??) *
)??* +
)??+ ,
{@@ 	
appAA 
.AA %
UseDeveloperExceptionPageAA )
(AA) *
)AA* +
;AA+ ,
varCC )
apiVersionDescriptionProviderCC -
=CC. /
appCC0 3
.CC3 4
ServicesCC4 <
.CC< =
GetRequiredServiceCC= O
<CCO P*
IApiVersionDescriptionProviderCCP n
>CCn o
(CCo p
)CCp q
;CCq r
appEE 
.EE 

UseSwaggerEE 
(EE 
)EE 
;EE 
appFF 
.FF 
UseSwaggerUIFF 
(FF 
optionsFF $
=>FF% '
{GG 
foreachHH 
(HH 
varHH 
descriptionHH (
inHH) +)
apiVersionDescriptionProviderHH, I
.HHI J"
ApiVersionDescriptionsHHJ `
)HH` a
{II 
optionsJJ 
.JJ 
SwaggerEndpointJJ +
(JJ+ ,
$"JJ, .
$strJJ. 7
{JJ7 8
descriptionJJ8 C
.JJC D
	GroupNameJJD M
}JJM N
$strJJN [
"JJ[ \
,JJ\ ]
descriptionKK #
.KK# $
	GroupNameKK$ -
.KK- .
ToUpperInvariantKK. >
(KK> ?
)KK? @
)KK@ A
;KKA B
}LL 
}MM 
)MM 
;MM 
}NN 	
appPP 
.PP 
UseHttpsRedirectionPP 
(PP  
)PP  !
;PP! "
appRR 
.RR 
UseAuthorizationRR 
(RR 
)RR 
;RR 
appTT 
.TT 
MapControllersTT 
(TT 
)TT 
;TT 
appVV 
.VV 
RunVV 
(VV 
)VV 
;VV 
}WW 
}XX ˙
vC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\RestApi\Middleware\GlobalExceptionHandlingMiddleware.cs
	namespace 	
RestApi
 
. 

Middleware 
{ 
public 

class -
!GlobalExceptionHandlingMiddleware 2
{ 
private 
readonly 
RequestDelegate (
_next) .
;. /
public

 -
!GlobalExceptionHandlingMiddleware

 0
(

0 1
RequestDelegate

1 @
next

A E
)

E F
{ 	
_next 
= 
next 
; 
} 	
public 
async 
Task 
InvokeAsync %
(% &
HttpContext& 1
context2 9
)9 :
{ 	
try 
{ 
await 
_next 
( 
context #
)# $
;$ %
} 
catch 
( #
CartValidationException *
ex+ -
)- .
{ 
await  
HandleExceptionAsync *
(* +
context+ 2
,2 3
ex4 6
,6 7
StatusCodes8 C
.C D
Status400BadRequestD W
)W X
;X Y
} 
catch 
( !
CartNotFoundException (
ex) +
)+ ,
{ 
await  
HandleExceptionAsync *
(* +
context+ 2
,2 3
ex4 6
,6 7
StatusCodes8 C
.C D
Status404NotFoundD U
)U V
;V W
} 
catch 
( 
RepositoryException &
ex' )
)) *
{ 
await  
HandleExceptionAsync *
(* +
context+ 2
,2 3
ex4 6
,6 7
StatusCodes8 C
.C D(
Status500InternalServerErrorD `
)` a
;a b
}   
catch!! 
(!! 
	Exception!! 
ex!! 
)!!  
{"" 
await##  
HandleExceptionAsync## *
(##* +
context##+ 2
,##2 3
ex##4 6
,##6 7
StatusCodes##8 C
.##C D(
Status500InternalServerError##D `
)##` a
;##a b
}$$ 
}%% 	
private'' 
async'' 
Task''  
HandleExceptionAsync'' /
(''/ 0
HttpContext''0 ;
context''< C
,''C D
	Exception''E N
	exception''O X
,''X Y
int''Z ]

statusCode''^ h
)''h i
{(( 	
context)) 
.)) 
Response)) 
.)) 

StatusCode)) '
=))( )

statusCode))* 4
;))4 5
context** 
.** 
Response** 
.** 
ContentType** (
=**) *
$str**+ =
;**= >
var,, 
errorResponse,, 
=,, 
new,,  #
{-- 
status.. 
=.. 

statusCode.. #
,..# $
message// 
=// 
	exception// #
.//# $
Message//$ +
,//+ ,
details00 
=00 
	exception00 #
is00$ &
RepositoryException00' :
?00; <
null00= A
:00B C
	exception00D M
.00M N

StackTrace00N X
}11 
;11 
await33 
context33 
.33 
Response33 "
.33" #
WriteAsJsonAsync33# 3
(333 4
errorResponse334 A
)33A B
;33B C
}44 	
}55 
}66 ™
gC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\RestApi\Controllers\V2\CartController.cs
	namespace 	
RestApi
 
. 
Controllers 
. 
V2  
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str +
)+ ,
], -
[ 

ApiVersion 
( 
$str 
) 
] 
public 

class 
CartController 
:  !
ControllerBase" 0
{ 
private 
readonly 
ICartService %
_cartService& 2
;2 3
public 
CartController 
( 
ICartService *
cartService+ 6
)6 7
{ 	
_cartService 
= 
cartService &
;& '
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
IActionResult 
GetCartInfo (
(( )
Guid) -
cartId. 4
)4 5
{ 	
var 
cart 
= 
_cartService #
.# $
GetCartInfo$ /
(/ 0
cartId0 6
)6 7
;7 8
return   
Ok   
(   
cart   
.   
Items    
)    !
;  ! "
}!! 	
[)) 	
HttpPost))	 
()) 
$str)) "
)))" #
]))# $
public** 
IActionResult** 
	AddToCart** &
(**& '
Guid**' +
cartId**, 2
,**2 3
[**4 5
FromBody**5 =
]**= >
CartItemDto**? J
item**K O
)**O P
{++ 	
_cartService,, 
.,, 
AddItemToCart,, &
(,,& '
cartId,,' -
,,,- .
item,,/ 3
),,3 4
;,,4 5
return-- 
Ok-- 
(-- 
)-- 
;-- 
}.. 	
[66 	

HttpDelete66	 
(66 
$str66 -
)66- .
]66. /
public77 
IActionResult77 

RemoveItem77 '
(77' (
Guid77( ,
cartId77- 3
,773 4
int775 8
itemId779 ?
)77? @
{88 	
_cartService99 
.99 
RemoveItemFromCart99 +
(99+ ,
cartId99, 2
,992 3
itemId994 :
)99: ;
;99; <
return:: 
Ok:: 
(:: 
):: 
;:: 
};; 	
[AA 	
HttpGetAA	 
(AA 
$strAA 
)AA 
]AA 
publicBB 
ActionResultBB 
<BB 
IEnumerableBB '
<BB' (
CartBB( ,
>BB, -
>BB- .
GetAllCartsBB/ :
(BB: ;
)BB; <
{CC 	
varDD 
cartsDD 
=DD 
_cartServiceDD $
.DD$ %
GetAllCartsDD% 0
(DD0 1
)DD1 2
;DD2 3
returnEE 
OkEE 
(EE 
cartsEE 
)EE 
;EE 
}FF 	
}GG 
}HH µn
gC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\RestApi\Messaging\CartMessageListener.cs
	namespace 	
RestApi
 
. 
	Messaging 
{ 
public 

class 
CartMessageListener $
:% &
BackgroundService' 8
,8 9
IDisposable: E
{ 
private 
readonly  
IServiceScopeFactory -
_scopeFactory. ;
;; <
private 
readonly 
RabbitMqSettings )
	_settings* 3
;3 4
private 
IConnection 
_connection '
;' (
private 
IChannel 
_channel !
;! "
private 
string 
_consumerTag #
;# $
private 
bool 
	_disposed 
; 
public 
CartMessageListener "
(" # 
IServiceScopeFactory# 7
scopeFactory8 D
,D E
IOptionsF N
<N O
RabbitMqSettingsO _
>_ `
optionsa h
)h i
{ 	
_scopeFactory 
= 
scopeFactory (
;( )
	_settings 
= 
options 
.  
Value  %
;% &
} 	
	protected 
override 
async  
Task! %
ExecuteAsync& 2
(2 3
CancellationToken3 D
stoppingTokenE R
)R S
{ 	
try   
{!! 
var"" 
factory"" 
="" 
new"" !
ConnectionFactory""" 3
{## 
HostName$$ 
=$$ 
	_settings$$ (
.$$( )
HostName$$) 1
,$$1 2
Port%% 
=%% 
	_settings%% $
.%%$ %
Port%%% )
,%%) *
UserName&& 
=&& 
	_settings&& (
.&&( )
UserName&&) 1
,&&1 2
Password'' 
='' 
	_settings'' (
.''( )
Password'') 1
}(( 
;(( 
_connection** 
=** 
await** #
factory**$ +
.**+ ,!
CreateConnectionAsync**, A
(**A B
)**B C
;**C D
_channel++ 
=++ 
await++  
_connection++! ,
.++, -
CreateChannelAsync++- ?
(++? @
)++@ A
;++A B
await-- 
_channel-- 
.-- 
QueueDeclareAsync-- 0
(--0 1
queue--1 6
:--6 7
$str--8 L
,--L M
durable..0 7
:..7 8
true..9 =
,..= >
	exclusive//0 9
://9 :
false//; @
,//@ A

autoDelete000 :
:00: ;
false00< A
,00A B
	arguments110 9
:119 :
null11; ?
)11? @
;11@ A
await33 
_channel33 
.33  
ExchangeDeclareAsync33 3
(333 4
exchange334 <
:33< =
$str33> N
,33N O
type33P T
:33T U
ExchangeType33V b
.33b c
Direct33c i
)33i j
;33j k
await55 
_channel55 
.55 
QueueBindAsync55 -
(55- .
queue55. 3
:553 4
$str555 I
,55I J
exchange66, 4
:664 5
$str666 F
,66F G

routingKey77, 6
:776 7
$str778 I
)77I J
;77J K
await99 
_channel99 
.99 
BasicQosAsync99 ,
(99, -
prefetchSize99- 9
:999 :
$num99; <
,99< =
prefetchCount99> K
:99K L
$num99M N
,99N O
global99P V
:99V W
false99X ]
)99] ^
;99^ _
var;; 
consumer;; 
=;; 
new;; "&
AsyncEventingBasicConsumer;;# =
(;;= >
_channel;;> F
);;F G
;;;G H
consumer== 
.== 
ReceivedAsync== &
+===' )
async==* /
(==0 1
model==1 6
,==6 7
ea==8 :
)==: ;
=>==< >
{>> 
if?? 
(?? 
stoppingToken?? %
.??% &#
IsCancellationRequested??& =
)??= >
{@@ 
ConsoleAA 
.AA  
	WriteLineAA  )
(AA) *
$strAA* k
)AAk l
;AAl m
awaitBB 
_channelBB &
.BB& '
BasicNackAsyncBB' 5
(BB5 6
eaBB6 8
.BB8 9
DeliveryTagBB9 D
,BBD E
multipleBBF N
:BBN O
falseBBP U
,BBU V
requeueBBW ^
:BB^ _
trueBB` d
)BBd e
;BBe f
returnCC 
;CC 
}DD 
varFF 
bodyFF 
=FF 
eaFF !
.FF! "
BodyFF" &
.FF& '
ToArrayFF' .
(FF. /
)FF/ 0
;FF0 1
varGG 
messageGG 
=GG  !
EncodingGG" *
.GG* +
UTF8GG+ /
.GG/ 0
	GetStringGG0 9
(GG9 :
bodyGG: >
)GG> ?
;GG? @
ConsoleII 
.II 
	WriteLineII %
(II% &
$"II& (
$strII( g
{IIg h
messageIIh o
}IIo p
"IIp q
)IIq r
;IIr s
tryKK 
{LL 
varMM 
productUpdateMM )
=MM* +
JsonSerializerMM, :
.MM: ;
DeserializeMM; F
<MMF G 
ProductUpdateMessageMMG [
>MM[ \
(MM\ ]
messageMM] d
)MMd e
;MMe f
ifOO 
(OO 
productUpdateOO )
!=OO* ,
nullOO- 1
)OO1 2
{PP 
usingQQ !
(QQ" #
varQQ# &
scopeQQ' ,
=QQ- .
_scopeFactoryQQ/ <
.QQ< =
CreateScopeQQ= H
(QQH I
)QQI J
)QQJ K
{RR 
varSS  #
cartServiceSS$ /
=SS0 1
scopeSS2 7
.SS7 8
ServiceProviderSS8 G
.SSG H
GetRequiredServiceSSH Z
<SSZ [
ICartServiceSS[ g
>SSg h
(SSh i
)SSi j
;SSj k
ConsoleUU  '
.UU' (
	WriteLineUU( 1
(UU1 2
$"UU2 4
$str	UU4 â
{
UUâ ä
DateTime
UUä í
.
UUí ì
UtcNow
UUì ô
}
UUô ö
$str
UUö õ
"
UUõ ú
)
UUú ù
;
UUù û
cartServiceWW  +
.WW+ ,
UpdateCartItemsWW, ;
(WW; <
	productIdXX$ -
:XX- .
productUpdateXX/ <
.XX< =
ItemIdXX= C
,XXC D
updatedNameYY$ /
:YY/ 0
productUpdateYY1 >
.YY> ?
NameYY? C
,YYC D
updatedPriceZZ$ 0
:ZZ0 1
productUpdateZZ2 ?
.ZZ? @
PriceZZ@ E
)[[  !
;[[! "
await\\  %
_channel\\& .
.\\. /
BasicAckAsync\\/ <
(\\< =
ea\\= ?
.\\? @
DeliveryTag\\@ K
,\\K L
multiple\\M U
:\\U V
false\\W \
)\\\ ]
;\\] ^
}]] 
}^^ 
else__ 
{`` 
Consoleaa #
.aa# $
	WriteLineaa$ -
(aa- .
$straa. t
)aat u
;aau v
awaitbb !
_channelbb" *
.bb* +
BasicAckAsyncbb+ 8
(bb8 9
eabb9 ;
.bb; <
DeliveryTagbb< G
,bbG H
multiplebbI Q
:bbQ R
falsebbS X
)bbX Y
;bbY Z
}cc 
}dd 
catchee 
(ee 
	Exceptionee $
exee% '
)ee' (
{ff 
Consolegg 
.gg  
	WriteLinegg  )
(gg) *
$"gg* ,
$strgg, V
{ggV W
exggW Y
.ggY Z
MessageggZ a
}gga b
"ggb c
)ggc d
;ggd e
awaithh 
_channelhh &
.hh& '
BasicNackAsynchh' 5
(hh5 6
eahh6 8
.hh8 9
DeliveryTaghh9 D
,hhD E
multiplehhF N
:hhN O
falsehhP U
,hhU V
requeuehhW ^
:hh^ _
truehh` d
)hhd e
;hhe f
}ii 
}jj 
;jj 
_consumerTagll 
=ll 
awaitll $
_channelll% -
.ll- .
BasicConsumeAsyncll. ?
(ll? @
queuell@ E
:llE F
$strllG [
,ll[ \
autoAckmm% ,
:mm, -
falsemm. 3
,mm3 4
consumernn% -
:nn- .
consumernn/ 7
)nn7 8
;nn8 9
}oo 
catchpp 
(pp &
OperationCanceledExceptionpp -
)pp- .
{qq 
Consolerr 
.rr 
	WriteLinerr !
(rr! "
$strrr" M
)rrM N
;rrN O
}ss 
catchtt 
(tt 
	Exceptiontt 
extt 
)tt  
{uu 
Consolevv 
.vv 
	WriteLinevv !
(vv! "
$"vv" $
$strvv$ G
{vvG H
exvvH J
.vvJ K
MessagevvK R
}vvR S
"vvS T
)vvT U
;vvU V
throwww 
;ww 
}xx 
}yy 	
public{{ 
override{{ 
async{{ 
Task{{ "
	StopAsync{{# ,
({{, -
CancellationToken{{- >
cancellationToken{{? P
){{P Q
{|| 	
Console}} 
.}} 
	WriteLine}} 
(}} 
$str}} A
)}}A B
;}}B C
try~~ 
{ 
if
ÄÄ 
(
ÄÄ 
_channel
ÄÄ 
!=
ÄÄ 
null
ÄÄ  $
&&
ÄÄ% '
!
ÄÄ( )
string
ÄÄ) /
.
ÄÄ/ 0
IsNullOrEmpty
ÄÄ0 =
(
ÄÄ= >
_consumerTag
ÄÄ> J
)
ÄÄJ K
)
ÄÄK L
{
ÅÅ 
await
ÇÇ 
_channel
ÇÇ "
.
ÇÇ" #
BasicCancelAsync
ÇÇ# 3
(
ÇÇ3 4
_consumerTag
ÇÇ4 @
)
ÇÇ@ A
;
ÇÇA B
Console
ÉÉ 
.
ÉÉ 
	WriteLine
ÉÉ %
(
ÉÉ% &
$str
ÉÉ& P
)
ÉÉP Q
;
ÉÉQ R
}
ÑÑ 
_channel
ÖÖ 
?
ÖÖ 
.
ÖÖ 

CloseAsync
ÖÖ $
(
ÖÖ$ %
cancellationToken
ÖÖ% 6
)
ÖÖ6 7
;
ÖÖ7 8
_connection
ÜÜ 
?
ÜÜ 
.
ÜÜ 

CloseAsync
ÜÜ '
(
ÜÜ' (
cancellationToken
ÜÜ( 9
)
ÜÜ9 :
;
ÜÜ: ;
}
áá 
catch
àà 
(
àà 
	Exception
àà 
ex
àà 
)
àà  
{
ââ 
Console
ää 
.
ää 
	WriteLine
ää !
(
ää! "
$"
ää" $
$str
ää$ Q
{
ääQ R
ex
ääR T
.
ääT U
Message
ääU \
}
ää\ ]
"
ää] ^
)
ää^ _
;
ää_ `
}
ãã 
await
åå 
base
åå 
.
åå 
	StopAsync
åå  
(
åå  !
cancellationToken
åå! 2
)
åå2 3
;
åå3 4
Console
çç 
.
çç 
	WriteLine
çç 
(
çç 
$str
çç >
)
çç> ?
;
çç? @
}
éé 	
	protected
êê 
void
êê 
Dispose
êê 
(
êê 
bool
êê #
	disposing
êê$ -
)
êê- .
{
ëë 	
if
íí 
(
íí 
	_disposed
íí 
)
íí 
{
ìì 
return
îî 
;
îî 
}
ïï 
if
óó 
(
óó 
	disposing
óó 
)
óó 
{
òò 
try
ôô 
{
öö 
_channel
õõ 
?
õõ 
.
õõ 
Dispose
õõ %
(
õõ% &
)
õõ& '
;
õõ' (
_connection
úú 
?
úú  
.
úú  !
Dispose
úú! (
(
úú( )
)
úú) *
;
úú* +
}
ùù 
catch
ûû 
(
ûû 
	Exception
ûû  
ex
ûû! #
)
ûû# $
{
üü 
Console
†† 
.
†† 
	WriteLine
†† %
(
††% &
$"
††& (
$str
††( b
{
††b c
ex
††c e
.
††e f
Message
††f m
}
††m n
"
††n o
)
††o p
;
††p q
}
°° 
}
¢¢ 
	_disposed
§§ 
=
§§ 
true
§§ 
;
§§ 
base
•• 
.
•• 
Dispose
•• 
(
•• 
)
•• 
;
•• 
}
¶¶ 	
public
®® 
override
®® 
void
®® 
Dispose
®® $
(
®®$ %
)
®®% &
{
©© 	
Dispose
™™ 
(
™™ 
true
™™ 
)
™™ 
;
™™ 
GC
´´ 
.
´´ 
SuppressFinalize
´´ 
(
´´  
this
´´  $
)
´´$ %
;
´´% &
}
¨¨ 	
private
ÆÆ 
class
ÆÆ "
ProductUpdateMessage
ÆÆ *
{
ØØ 	
public
∞∞ 
int
∞∞ 
ItemId
∞∞ 
{
∞∞ 
get
∞∞  #
;
∞∞# $
set
∞∞% (
;
∞∞( )
}
∞∞* +
public
±± 
string
±± 
Name
±± 
{
±±  
get
±±! $
;
±±$ %
set
±±& )
;
±±) *
}
±±+ ,
public
≤≤ 
decimal
≤≤ 
Price
≤≤  
{
≤≤! "
get
≤≤# &
;
≤≤& '
set
≤≤( +
;
≤≤+ ,
}
≤≤- .
}
≥≥ 	
}
¥¥ 
}µµ ÷G
jC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\RestApi\Controllers\V1\ProductController.cs
	namespace 	
RestApi
 
. 
Controllers 
. 
V1  
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 3
)3 4
]4 5
[		 

ApiVersion		 
(		 
$str		 
)		 
]		 
public

 

class

 
ProductController

 "
:

# $
ControllerBase

% 3
{ 
private 
readonly 
IProductService (
_productService) 8
;8 9
public 
ProductController  
(  !
IProductService! 0
productService1 ?
)? @
{ 	
_productService 
= 
productService ,
;, -
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetById) 0
(0 1
int1 4
id5 7
)7 8
{ 	
var 
product 
= 
await 
_productService  /
./ 0
GetByIdAsync0 <
(< =
id= ?
)? @
;@ A
if 
( 
product 
== 
null 
)  
{ 
return 
NotFound 
(  
)  !
;! "
} 
var 
selfLink 
= 
Url 
. 
Action %
(% &
nameof& ,
(, -
GetById- 4
)4 5
,5 6
$str7 @
,@ A
newB E
{F G
idH J
=K L
productM T
.T U
IdU W
}X Y
,Y Z
Request[ b
.b c
Schemec i
,i j
Requestk r
.r s
Hosts w
.w x
Valuex }
)} ~
;~ 
var 

updateLink 
= 
Url  
.  !
Action! '
(' (
nameof( .
(. /
Update/ 5
)5 6
,6 7
$str8 A
,A B
newC F
{G H
idI K
=L M
productN U
.U V
IdV X
}Y Z
,Z [
Request\ c
.c d
Schemed j
,j k
Requestl s
.s t
Hostt x
.x y
Valuey ~
)~ 
;	 Ä
var 

deleteLink 
= 
Url  
.  !
Action! '
(' (
nameof( .
(. /
Delete/ 5
)5 6
,6 7
$str8 A
,A B
newC F
{G H
idI K
=L M
productN U
.U V
IdV X
}Y Z
,Z [
Request\ c
.c d
Schemed j
,j k
Requestl s
.s t
Hostt x
.x y
Valuey ~
)~ 
;	 Ä
Response   
.   
Headers   
.   
Append   #
(  # $
$str  $ *
,  * +
$"  , .
$str  . /
{  / 0
selfLink  0 8
}  8 9
$str  9 X
"  X Y
)  Y Z
;  Z [
Response!! 
.!! 
Headers!! 
.!! 
Append!! #
(!!# $
$str!!$ *
,!!* +
$"!!, .
$str!!. /
{!!/ 0

updateLink!!0 :
}!!: ;
$str!!; \
"!!\ ]
)!!] ^
;!!^ _
Response"" 
."" 
Headers"" 
."" 
Append"" #
(""# $
$str""$ *
,""* +
$""", .
$str"". /
{""/ 0

deleteLink""0 :
}"": ;
$str""; _
"""_ `
)""` a
;""a b
var$$ 

productDto$$ 
=$$ 
new$$  

ProductDto$$! +
{%% 
Id&& 
=&& 
product&& 
.&& 
Id&& 
,&&  
Name'' 
='' 
product'' 
.'' 
Name'' #
,''# $
Description(( 
=(( 
product(( %
.((% &
Description((& 1
,((1 2
Image)) 
=)) 
product)) 
.))  
Image))  %
,))% &
Price** 
=** 
product** 
.**  
Price**  %
,**% &
Amount++ 
=++ 
product++  
.++  !
Amount++! '
,++' (

CategoryId,, 
=,, 
product,, $
.,,$ %

CategoryId,,% /
}-- 
;-- 
return// 
Ok// 
(// 

productDto//  
)//  !
;//! "
}00 	
[22 	
HttpGet22	 
]22 
public33 
async33 
Task33 
<33 
IActionResult33 '
>33' ("
ListProductsByCategory33) ?
(33? @
[33@ A
	FromQuery33A J
]33J K
int33L O
?33O P

categoryId33Q [
,33[ \
[33] ^
	FromQuery33^ g
]33g h
int33i l
page33m q
=33r s
$num33t u
,33u v
[33w x
	FromQuery	33x Å
]
33Å Ç
int
33É Ü
pageSize
33á è
=
33ê ë
$num
33í î
)
33î ï
{44 	
var55 
products55 
=55 
await55  
_productService55! 0
.550 1
	ListAsync551 :
(55: ;

categoryId55; E
,55E F
page55G K
,55K L
pageSize55M U
)55U V
;55V W
return66 
Ok66 
(66 
products66 
)66 
;66  
}77 	
[99 	
HttpPost99	 
]99 
public:: 
async:: 
Task:: 
<:: 
IActionResult:: '
>::' (
Add::) ,
(::, -

ProductDto::- 7

productDto::8 B
)::B C
{;; 	
if<< 
(<< 
!<< 

ModelState<< 
.<< 
IsValid<< #
)<<# $
return== 

BadRequest== !
(==! "

ModelState==" ,
)==, -
;==- .
var?? 
res?? 
=?? 
await?? 
_productService?? +
.??+ ,
AddAsync??, 4
(??4 5

productDto??5 ?
)??? @
;??@ A
return@@ 
CreatedAtAction@@ "
(@@" #
nameof@@# )
(@@) *
GetById@@* 1
)@@1 2
,@@2 3
new@@4 7
{@@8 9
id@@: <
=@@= >
res@@? B
.@@B C
Id@@C E
}@@F G
,@@G H
res@@I L
)@@L M
;@@M N
}AA 	
[CC 	
HttpPutCC	 
(CC 
$strCC 
)CC 
]CC 
publicDD 
asyncDD 
TaskDD 
<DD 
IActionResultDD '
>DD' (
UpdateDD) /
(DD/ 0
intDD0 3
idDD4 6
,DD6 7
[DD8 9
FromBodyDD9 A
]DDA B

ProductDtoDDC M

productDtoDDN X
)DDX Y
{EE 	
ifFF 
(FF 
idFF 
!=FF 

productDtoFF  
.FF  !
IdFF! #
)FF# $
returnGG 

BadRequestGG !
(GG! "
$strGG" _
)GG_ `
;GG` a
ifII 
(II 
!II 

ModelStateII 
.II 
IsValidII #
)II# $
returnJJ 

BadRequestJJ !
(JJ! "

ModelStateJJ" ,
)JJ, -
;JJ- .
awaitLL 
_productServiceLL !
.LL! "
UpdateAsyncLL" -
(LL- .

productDtoLL. 8
)LL8 9
;LL9 :
returnMM 
	NoContentMM 
(MM 
)MM 
;MM 
}NN 	
[PP 	

HttpDeletePP	 
(PP 
$strPP 
)PP 
]PP 
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
IActionResultQQ '
>QQ' (
DeleteQQ) /
(QQ/ 0
intQQ0 3
idQQ4 6
)QQ6 7
{RR 	
awaitSS 
_productServiceSS !
.SS! "
DeleteAsyncSS" -
(SS- .
idSS. 0
)SS0 1
;SS1 2
returnTT 
	NoContentTT 
(TT 
)TT 
;TT 
}UU 	
}VV 
}WW »)
kC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\RestApi\Controllers\V1\CategoryController.cs
	namespace 	
RestApi
 
. 
Controllers 
. 
V1  
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 3
)3 4
]4 5
[		 

ApiVersion		 
(		 
$str		 
)		 
]		 
public

 

class

 
CategoryController

 #
:

$ %
ControllerBase

& 4
{ 
private 
readonly 
ICategoryService )
_categoryService* :
;: ;
public 
CategoryController !
(! "
ICategoryService" 2
categoryService3 B
)B C
{ 	
_categoryService 
= 
categoryService .
;. /
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
ListAll) 0
(0 1
)1 2
{ 	
var 

categories 
= 
await "
_categoryService# 3
.3 4
	ListAsync4 =
(= >
)> ?
;? @
return 
Ok 
( 

categories  
)  !
;! "
} 	
[ 	
HttpPost	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
Add) ,
(, -
CategoryDto- 8
categoryDto9 D
)D E
{ 	
if 
( 
! 

ModelState 
. 
IsValid #
)# $
return 

BadRequest !
(! "

ModelState" ,
), -
;- .
var   
createdCategory   
=    !
await  " '
_categoryService  ( 8
.  8 9
AddAsync  9 A
(  A B
categoryDto  B M
)  M N
;  N O
return!! 
CreatedAtAction!! "
(!!" #
nameof!!# )
(!!) *
GetById!!* 1
)!!1 2
,!!2 3
new!!4 7
{!!8 9
id!!: <
=!!= >
createdCategory!!? N
.!!N O
Id!!O Q
}!!R S
,!!S T
createdCategory!!U d
)!!d e
;!!e f
}"" 	
[$$ 	
HttpGet$$	 
($$ 
$str$$ 
)$$ 
]$$ 
public%% 
async%% 
Task%% 
<%% 
IActionResult%% '
>%%' (
GetById%%) 0
(%%0 1
int%%1 4
id%%5 7
)%%7 8
{&& 	
var'' 
category'' 
='' 
await''  
_categoryService''! 1
.''1 2
GetByIdAsync''2 >
(''> ?
id''? A
)''A B
;''B C
if(( 
((( 
category(( 
==(( 
null((  
)((  !
return)) 
NotFound)) 
())  
)))  !
;))! "
return** 
Ok** 
(** 
category** 
)** 
;**  
}++ 	
[-- 	
HttpPut--	 
(-- 
$str-- 
)-- 
]-- 
public.. 
async.. 
Task.. 
<.. 
IActionResult.. '
>..' (
Update..) /
(../ 0
int..0 3
id..4 6
,..6 7
CategoryDto..8 C
categoryDto..D O
)..O P
{// 	
if00 
(00 
!00 

ModelState00 
.00 
IsValid00 #
||00$ &
id00' )
!=00* ,
categoryDto00- 8
.008 9
Id009 ;
)00; <
return11 

BadRequest11 !
(11! "
)11" #
;11# $
await33 
_categoryService33 "
.33" #
UpdateAsync33# .
(33. /
categoryDto33/ :
)33: ;
;33; <
return44 
	NoContent44 
(44 
)44 
;44 
}55 	
[77 	

HttpDelete77	 
(77 
$str77 
)77 
]77 
public88 
async88 
Task88 
<88 
IActionResult88 '
>88' (
Delete88) /
(88/ 0
int880 3
id884 6
)886 7
{99 	
try:: 
{;; 
await<< 
_categoryService<< &
.<<& '
DeleteAsync<<' 2
(<<2 3
id<<3 5
)<<5 6
;<<6 7
return== 
	NoContent==  
(==  !
)==! "
;==" #
}>> 
catch?? 
(?? 
	Exception?? 
ex?? 
)??  
{@@ 
returnAA 

BadRequestAA !
(AA! "
exAA" $
.AA$ %
MessageAA% ,
)AA, -
;AA- .
}BB 
}CC 	
}DD 
}EE õ
gC:\netMentoring\04_layered_architectures\CartServiceConsoleApp\RestApi\Controllers\V1\CartController.cs
	namespace 	
RestApi
 
. 
Controllers 
. 
V1  
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str +
)+ ,
], -
[ 

ApiVersion 
( 
$str 
) 
] 
public 

class 
CartController 
:  !
ControllerBase" 0
{ 
private 
readonly 
ICartService %
_cartService& 2
;2 3
public 
CartController 
( 
ICartService *
cartService+ 6
)6 7
{ 	
_cartService 
= 
cartService &
;& '
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
IActionResult 
GetCartInfo (
(( )
Guid) -
cartId. 4
)4 5
{ 	
var 
cart 
= 
_cartService #
.# $
GetCartInfo$ /
(/ 0
cartId0 6
)6 7
;7 8
return   
Ok   
(   
cart   
)   
;   
}!! 	
[)) 	
HttpPost))	 
()) 
$str)) "
)))" #
]))# $
public** 
async** 
Task** 
<** 
IActionResult** '
>**' (
	AddToCart**) 2
(**2 3
Guid**3 7
cartId**8 >
,**> ?
[**@ A
FromBody**A I
]**I J
CartItemDto**K V
item**W [
,**[ \
CancellationToken**] n
cancellationToken	**o Ä
)
**Ä Å
{++ 	
_cartService,, 
.,, 
AddItemToCart,, &
(,,& '
cartId,,' -
,,,- .
item,,/ 3
),,3 4
;,,4 5
return.. 
Ok.. 
(.. 
).. 
;.. 
}// 	
[77 	

HttpDelete77	 
(77 
$str77 -
)77- .
]77. /
public88 
IActionResult88 

RemoveItem88 '
(88' (
Guid88( ,
cartId88- 3
,883 4
int885 8
itemId889 ?
)88? @
{99 	
_cartService:: 
.:: 
RemoveItemFromCart:: +
(::+ ,
cartId::, 2
,::2 3
itemId::4 :
)::: ;
;::; <
return;; 
Ok;; 
(;; 
);; 
;;; 
}<< 	
[BB 	
HttpGetBB	 
(BB 
$strBB 
)BB 
]BB 
publicCC 
ActionResultCC 
<CC 
IEnumerableCC '
<CC' (
CartCC( ,
>CC, -
>CC- .
GetAllCartsCC/ :
(CC: ;
)CC; <
{DD 	
varEE 
cartsEE 
=EE 
_cartServiceEE $
.EE$ %
GetAllCartsEE% 0
(EE0 1
)EE1 2
;EE2 3
returnFF 
OkFF 
(FF 
cartsFF 
)FF 
;FF 
}GG 	
}HH 
}II 