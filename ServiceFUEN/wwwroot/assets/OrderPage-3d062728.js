import{_ as g,u as v,o as c,c as a,F as p,a as h,e as r,g as _,b as s,t as e,p as f,j as y}from"./index-9fdc7868.js";import{_ as b}from"./Spinner-1s-200px-2-0335de7c.js";const x={mixins:[v],data(){return{orderdetail:[],orderitems:[],orderid:0,showprodu:"",noorder:"",itemid:"",isempty:!1,isloading:!0}},mounted(){this.getOD()},created(){},methods:{async getOD(){let o=await this.fetchMemberId();this.isloading=!0,this.isempty=!1,axios.get(`https://localhost:7259/api/OrderDetail/GetMemberOrder?memberid=${o}`).then(l=>{this.isloading=!1,l.data.length>0?(console.log(l.data),this.orderdetail=l.data):this.isempty=!0}).catch(l=>{})}}},d=o=>(f("data-v-33838f58"),o=o(),y(),o),I={class:"content"},w=d(()=>s("h4",null,"我的訂單紀錄",-1)),O=d(()=>s("div",{class:"line mb-4"},null,-1)),D=["data-bs-target"],S=d(()=>s("div",{class:"row align-items-center"},[s("div",{class:"col-1"},[s("i",{class:"fa-solid fa-clipboard"})])],-1)),k={class:"row"},P=d(()=>s("div",{class:"col-1"},null,-1)),j={class:"col-8"},B={class:""},F={class:""},N={class:""},$={class:"col-3"},M={class:"totalloca"},C=["id"],E={class:"col-3"},G=["src"],L={class:"col-5"},V={class:"col-2"},q={class:"col-2"},z={class:"image-container"},A=d(()=>s("img",{src:b,alt:""},null,-1)),H=[A];function J(o,l,K,Q,i,R){return c(),a("div",I,[w,O,(c(!0),a(p,null,h(i.orderdetail,(t,u)=>r((c(),a("div",{class:"outline",key:u},[s("div",{class:"prooutline container-fluid","data-bs-toggle":"collapse","data-bs-target":`#index${t.id}`},[S,s("div",k,[P,s("div",j,[s("p",B,"訂單編號:"+e(t.paymentId),1),s("p",F,"優惠券:"+e(t.usedCoupon),1),s("p",N,"收件資訊: "+e(t.address),1)]),s("div",$,[s("p",M,"總金額: $"+e(t.total),1)])])],8,D),s("div",{class:"col-10 prooutline2 container-fluid collapsing justify-content-center align-items-center",id:`index${t.id}`},[(c(!0),a(p,null,h(t.orderItems,(n,m)=>(c(),a("div",{class:"row procard d-flex justify-content-center align-items-center",key:m},[s("div",E,[s("img",{class:"pic",src:"https://localhost:7027/ProductImgFiles/"+n.source,alt:""},null,8,G)]),s("div",L,[s("small",null,e(n.productName),1)]),s("div",V,[s("small",null,"$"+e(n.productPrice),1)]),s("div",q,[s("small",null,"數量"+e(n.productNumber),1)])]))),128))],8,C)])),[[_,!i.isempty&&!i.isloading]])),128)),r(s("div",null,"目前沒有訂單~",512),[[_,i.isempty&&!i.isloading]]),r(s("div",z,H,512),[[_,i.isloading]])])}const W=g(x,[["render",J],["__scopeId","data-v-33838f58"]]);export{W as default};
