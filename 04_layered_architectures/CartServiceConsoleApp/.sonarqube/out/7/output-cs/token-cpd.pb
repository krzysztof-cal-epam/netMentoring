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
}66 ∏h
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
;# $
public 
CartMessageListener "
(" # 
IServiceScopeFactory# 7
scopeFactory8 D
,D E
IOptionsF N
<N O
RabbitMqSettingsO _
>_ `
optionsa h
)h i
{ 	
_scopeFactory 
= 
scopeFactory (
;( )
	_settings 
= 
options 
.  
Value  %
;% &
} 	
	protected 
override 
async  
Task! %
ExecuteAsync& 2
(2 3
CancellationToken3 D
stoppingTokenE R
)R S
{ 	
try 
{ 
var   
factory   
=   
new   !
ConnectionFactory  " 3
{!! 
HostName"" 
="" 
	_settings"" (
.""( )
HostName"") 1
,""1 2
Port## 
=## 
	_settings## $
.##$ %
Port##% )
,##) *
UserName$$ 
=$$ 
	_settings$$ (
.$$( )
UserName$$) 1
,$$1 2
Password%% 
=%% 
	_settings%% (
.%%( )
Password%%) 1
}&& 
;&& 
_connection(( 
=(( 
await(( #
factory(($ +
.((+ ,!
CreateConnectionAsync((, A
(((A B
)((B C
;((C D
_channel)) 
=)) 
await))  
_connection))! ,
.)), -
CreateChannelAsync))- ?
())? @
)))@ A
;))A B
await++ 
_channel++ 
.++ 
QueueDeclareAsync++ 0
(++0 1
queue++1 6
:++6 7
$str++8 L
,++L M
durable,,0 7
:,,7 8
true,,9 =
,,,= >
	exclusive--0 9
:--9 :
false--; @
,--@ A

autoDelete..0 :
:..: ;
false..< A
,..A B
	arguments//0 9
://9 :
null//; ?
)//? @
;//@ A
await11 
_channel11 
.11  
ExchangeDeclareAsync11 3
(113 4
exchange114 <
:11< =
$str11> N
,11N O
type11P T
:11T U
ExchangeType11V b
.11b c
Direct11c i
)11i j
;11j k
await33 
_channel33 
.33 
QueueBindAsync33 -
(33- .
queue33. 3
:333 4
$str335 I
,33I J
exchange44, 4
:444 5
$str446 F
,44F G

routingKey55, 6
:556 7
$str558 I
)55I J
;55J K
await77 
_channel77 
.77 
BasicQosAsync77 ,
(77, -
prefetchSize77- 9
:779 :
$num77; <
,77< =
prefetchCount77> K
:77K L
$num77M N
,77N O
global77P V
:77V W
false77X ]
)77] ^
;77^ _
var99 
consumer99 
=99 
new99 "&
AsyncEventingBasicConsumer99# =
(99= >
_channel99> F
)99F G
;99G H
consumer;; 
.;; 
ReceivedAsync;; &
+=;;' )
async;;* /
(;;0 1
model;;1 6
,;;6 7
ea;;8 :
);;: ;
=>;;< >
{<< 
if== 
(== 
stoppingToken== %
.==% &#
IsCancellationRequested==& =
)=== >
{>> 
Console?? 
.??  
	WriteLine??  )
(??) *
$str??* k
)??k l
;??l m
await@@ 
_channel@@ &
.@@& '
BasicNackAsync@@' 5
(@@5 6
ea@@6 8
.@@8 9
DeliveryTag@@9 D
,@@D E
multiple@@F N
:@@N O
false@@P U
,@@U V
requeue@@W ^
:@@^ _
true@@` d
)@@d e
;@@e f
returnAA 
;AA 
}BB 
varDD 
bodyDD 
=DD 
eaDD !
.DD! "
BodyDD" &
.DD& '
ToArrayDD' .
(DD. /
)DD/ 0
;DD0 1
varEE 
messageEE 
=EE  !
EncodingEE" *
.EE* +
UTF8EE+ /
.EE/ 0
	GetStringEE0 9
(EE9 :
bodyEE: >
)EE> ?
;EE? @
ConsoleGG 
.GG 
	WriteLineGG %
(GG% &
$"GG& (
$strGG( g
{GGg h
messageGGh o
}GGo p
"GGp q
)GGq r
;GGr s
tryII 
{JJ 
varKK 
productUpdateKK )
=KK* +
JsonSerializerKK, :
.KK: ;
DeserializeKK; F
<KKF G 
ProductUpdateMessageKKG [
>KK[ \
(KK\ ]
messageKK] d
)KKd e
;KKe f
ifMM 
(MM 
productUpdateMM )
!=MM* ,
nullMM- 1
)MM1 2
{NN 
usingOO !
(OO" #
varOO# &
scopeOO' ,
=OO- .
_scopeFactoryOO/ <
.OO< =
CreateScopeOO= H
(OOH I
)OOI J
)OOJ K
{PP 
varQQ  #
cartServiceQQ$ /
=QQ0 1
scopeQQ2 7
.QQ7 8
ServiceProviderQQ8 G
.QQG H
GetRequiredServiceQQH Z
<QQZ [
ICartServiceQQ[ g
>QQg h
(QQh i
)QQi j
;QQj k
ConsoleSS  '
.SS' (
	WriteLineSS( 1
(SS1 2
$"SS2 4
$str	SS4 â
{
SSâ ä
DateTime
SSä í
.
SSí ì
UtcNow
SSì ô
}
SSô ö
$str
SSö õ
"
SSõ ú
)
SSú ù
;
SSù û
cartServiceUU  +
.UU+ ,
UpdateCartItemsUU, ;
(UU; <
	productIdVV$ -
:VV- .
productUpdateVV/ <
.VV< =
ItemIdVV= C
,VVC D
updatedNameWW$ /
:WW/ 0
productUpdateWW1 >
.WW> ?
NameWW? C
,WWC D
updatedPriceXX$ 0
:XX0 1
productUpdateXX2 ?
.XX? @
PriceXX@ E
)YY  !
;YY! "
awaitZZ  %
_channelZZ& .
.ZZ. /
BasicAckAsyncZZ/ <
(ZZ< =
eaZZ= ?
.ZZ? @
DeliveryTagZZ@ K
,ZZK L
multipleZZM U
:ZZU V
falseZZW \
)ZZ\ ]
;ZZ] ^
}[[ 
}\\ 
else]] 
{^^ 
Console__ #
.__# $
	WriteLine__$ -
(__- .
$str__. t
)__t u
;__u v
await`` !
_channel``" *
.``* +
BasicAckAsync``+ 8
(``8 9
ea``9 ;
.``; <
DeliveryTag``< G
,``G H
multiple``I Q
:``Q R
false``S X
)``X Y
;``Y Z
}aa 
}bb 
catchcc 
(cc 
	Exceptioncc $
excc% '
)cc' (
{dd 
Consoleee 
.ee  
	WriteLineee  )
(ee) *
$"ee* ,
$stree, V
{eeV W
exeeW Y
.eeY Z
MessageeeZ a
}eea b
"eeb c
)eec d
;eed e
awaitff 
_channelff &
.ff& '
BasicNackAsyncff' 5
(ff5 6
eaff6 8
.ff8 9
DeliveryTagff9 D
,ffD E
multipleffF N
:ffN O
falseffP U
,ffU V
requeueffW ^
:ff^ _
trueff` d
)ffd e
;ffe f
}gg 
}hh 
;hh 
_consumerTagjj 
=jj 
awaitjj $
_channeljj% -
.jj- .
BasicConsumeAsyncjj. ?
(jj? @
queuejj@ E
:jjE F
$strjjG [
,jj[ \
autoAckkk% ,
:kk, -
falsekk. 3
,kk3 4
consumerll% -
:ll- .
consumerll/ 7
)ll7 8
;ll8 9
}mm 
catchnn 
(nn &
OperationCanceledExceptionnn -
)nn- .
{oo 
Consolepp 
.pp 
	WriteLinepp !
(pp! "
$strpp" M
)ppM N
;ppN O
}qq 
catchrr 
(rr 
	Exceptionrr 
exrr 
)rr  
{ss 
Consolett 
.tt 
	WriteLinett !
(tt! "
$"tt" $
$strtt$ G
{ttG H
exttH J
.ttJ K
MessagettK R
}ttR S
"ttS T
)ttT U
;ttU V
throwuu 
;uu 
}vv 
}ww 	
publicyy 
overrideyy 
asyncyy 
Taskyy "
	StopAsyncyy# ,
(yy, -
CancellationTokenyy- >
cancellationTokenyy? P
)yyP Q
{zz 	
Console{{ 
.{{ 
	WriteLine{{ 
({{ 
$str{{ A
){{A B
;{{B C
try|| 
{}} 
if~~ 
(~~ 
_channel~~ 
!=~~ 
null~~  $
&&~~% '
!~~( )
string~~) /
.~~/ 0
IsNullOrEmpty~~0 =
(~~= >
_consumerTag~~> J
)~~J K
)~~K L
{ 
await
ÄÄ 
_channel
ÄÄ "
.
ÄÄ" #
BasicCancelAsync
ÄÄ# 3
(
ÄÄ3 4
_consumerTag
ÄÄ4 @
)
ÄÄ@ A
;
ÄÄA B
Console
ÅÅ 
.
ÅÅ 
	WriteLine
ÅÅ %
(
ÅÅ% &
$str
ÅÅ& P
)
ÅÅP Q
;
ÅÅQ R
}
ÇÇ 
_channel
ÉÉ 
?
ÉÉ 
.
ÉÉ 

CloseAsync
ÉÉ $
(
ÉÉ$ %
cancellationToken
ÉÉ% 6
)
ÉÉ6 7
;
ÉÉ7 8
_connection
ÑÑ 
?
ÑÑ 
.
ÑÑ 

CloseAsync
ÑÑ '
(
ÑÑ' (
cancellationToken
ÑÑ( 9
)
ÑÑ9 :
;
ÑÑ: ;
}
ÖÖ 
catch
ÜÜ 
(
ÜÜ 
	Exception
ÜÜ 
ex
ÜÜ 
)
ÜÜ  
{
áá 
Console
àà 
.
àà 
	WriteLine
àà !
(
àà! "
$"
àà" $
$str
àà$ Q
{
ààQ R
ex
ààR T
.
ààT U
Message
ààU \
}
àà\ ]
"
àà] ^
)
àà^ _
;
àà_ `
}
ââ 
await
ää 
base
ää 
.
ää 
	StopAsync
ää  
(
ää  !
cancellationToken
ää! 2
)
ää2 3
;
ää3 4
Console
ãã 
.
ãã 
	WriteLine
ãã 
(
ãã 
$str
ãã >
)
ãã> ?
;
ãã? @
}
åå 	
	protected
éé 
void
éé 
Dispose
éé 
(
éé 
bool
éé #
	disposing
éé$ -
)
éé- .
{
èè 	
if
êê 
(
êê 
	disposing
êê 
)
êê 
{
ëë 
try
íí 
{
ìì 
_channel
îî 
?
îî 
.
îî 
Dispose
îî %
(
îî% &
)
îî& '
;
îî' (
_connection
ïï 
?
ïï  
.
ïï  !
Dispose
ïï! (
(
ïï( )
)
ïï) *
;
ïï* +
}
ññ 
catch
óó 
(
óó 
	Exception
óó  
ex
óó! #
)
óó# $
{
òò 
Console
ôô 
.
ôô 
	WriteLine
ôô %
(
ôô% &
$"
ôô& (
$str
ôô( b
{
ôôb c
ex
ôôc e
.
ôôe f
Message
ôôf m
}
ôôm n
"
ôôn o
)
ôôo p
;
ôôp q
}
öö 
}
õõ 
base
úú 
.
úú 
Dispose
úú 
(
úú 
)
úú 
;
úú 
}
ùù 	
private
üü 
class
üü "
ProductUpdateMessage
üü *
{
†† 	
public
°° 
int
°° 
ItemId
°° 
{
°° 
get
°°  #
;
°°# $
set
°°% (
;
°°( )
}
°°* +
public
¢¢ 
string
¢¢ 
Name
¢¢ 
{
¢¢  
get
¢¢! $
;
¢¢$ %
set
¢¢& )
;
¢¢) *
}
¢¢+ ,
public
££ 
decimal
££ 
Price
££  
{
££! "
get
££# &
;
££& '
set
££( +
;
££+ ,
}
££- .
}
§§ 	
}
•• 
}¶¶ ™
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
}HH ÷G
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