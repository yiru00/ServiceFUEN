import{a as p}from"./axios-aba6f0e0.js";import{_ as v,u,r as m,o as n,c as _,e as a,g as l,b as s,F as f,a as g,d as b,w as y,t as d,p as F,j as I,h as w}from"./index-9fdc7868.js";import{_ as x}from"./Spinner-1s-200px-2-0335de7c.js";const k={mixins:[u],data(){return{fav:[],isempty:!1,isloading:!0}},created(){this.CallFavoriteAll()},methods:{async CallFavoriteAll(){this.isempty=!1,this.loading=!0;let e=await this.fetchMemberId();await p.get(`https://localhost:7259/api/Favorites/FavoritesAll?memberId=${e}`).then(o=>{this.isloading=!1,this.fav=o.data,this.fav.length==0&&(this.isempty=!0)}).catch(o=>{console.log(o)})}}},c=e=>(F("data-v-31b88b78"),e=e(),I(),e),C={class:"content"},N=c(()=>s("h4",null,"我的收藏商品",-1)),S=c(()=>s("div",{class:"line mb-4"},null,-1)),A={class:"list"},B={class:"listContent"},D={class:"coverImg"},V=["src"],P={class:"info"},T={class:"productName"},j={class:"price"},E={class:"line"},L=c(()=>s("div",{class:"content"},[s("h4",null,"我的收藏商品"),s("div",{class:"line mb-4"}),w(" 沒有收藏的活動 ")],-1)),M=[L],q={class:"image-container"},z=c(()=>s("img",{src:x,alt:""},null,-1)),G=[z];function H(e,o,J,K,t,O){const h=m("router-link");return n(),_("div",null,[a(s("div",null,[s("div",C,[N,S,s("div",A,[(n(!0),_(f,null,g(t.fav,(i,r)=>(n(),_("div",{key:r},[b(h,{to:"/Product/"+i.id},{default:y(()=>[s("div",B,[s("div",D,[s("img",{src:"https://localhost:7027/ProductImgFiles/"+i.source,alt:"封面圖"},null,8,V)]),s("div",P,[s("p",T,d(i.name),1),s("p",j,"NTD "+d(i.price),1)])]),a(s("div",E,null,512),[[l,r+1<t.fav.length]])]),_:2},1032,["to"])]))),128))])])],512),[[l,!t.isempty&&!t.isloading]]),a(s("div",null,M,512),[[l,t.isempty&&!t.isloading]]),a(s("div",q,G,512),[[l,t.isloading]])])}const W=v(k,[["render",H],["__scopeId","data-v-31b88b78"]]);export{W as default};
