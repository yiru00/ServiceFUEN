import{_ as f,u as I,r as J,o as c,a,w as d,C as r,b as t,F as u,l as m,t as l,m as S,q as b,y as A,z as k,A as w}from"./index-3f0bd573.js";import{_ as x}from"./Spinner-1s-200px-2-0335de7c.js";const N={mixins:[I],computed:{groupedJoined(){const e={};return this.joinedData.forEach(o=>{const s=o.dateJoined.substring(0,7);e[s]||(e[s]=[]),e[s].push(o)}),e}},data(){return{joinedData:[],isempty:!1,isloading:!0}},mounted(){this.scrollToTop(),this.getActivityJoined()},methods:{async getActivityJoined(){this.isempty=!1,this.isloading=!0;const o={memberId:await this.fetchMemberId()};fetch("https://localhost:7259/api/ActivityRecord/Joined",{method:"POST",headers:{"Content-Type":"application/json"},body:JSON.stringify(o)}).then(s=>s.json()).then(s=>{this.isloading=!1,console.log("Success:",s),s.length==0&&(this.isempty=!0),this.joinedData=s}).catch(s=>{console.error("Error:",s)})}}},v=e=>(k("data-v-364edd41"),e=e(),w(),e),T={class:"relative"},j={class:"month"},C={class:"list"},D={class:"listContent"},B={class:"coverImg"},E=["src"],V={class:"info"},F={class:"activityName"},O={class:"description"},q={class:"date"},z=v(()=>t("i",{class:"fa-solid fa-calendar-days"},null,-1)),L={class:"line"},M={class:"image-container"},P=v(()=>t("img",{src:x,alt:""},null,-1)),R=[P];function G(e,o,s,H,i,g){const y=J("router-link");return c(),a("div",T,[d(t("div",null,[(c(!0),a(u,null,m(g.groupedJoined,(_,h)=>(c(),a("div",{key:h},[t("p",j,l(h),1),t("div",C,[(c(!0),a(u,null,m(_,(n,p)=>(c(),a("div",{key:p},[S(y,{to:"/Activity/"+n.activityId},{default:b(()=>[t("div",D,[t("div",B,[t("img",{src:n.coverImage,alt:"活動圖"},null,8,E)]),t("div",V,[t("p",F,l(n.activityName),1),t("p",O,l(n.description.slice(0,20))+"... ",1),t("p",q,[z,A(l(n.gatheringTime),1)])])]),d(t("div",L,null,512),[[r,p+1<_.length]])]),_:2},1032,["to"])]))),128))])]))),128))],512),[[r,!i.isempty&&!i.isloading]]),d(t("div",null,"沒有參加過的的活動",512),[[r,i.isempty&&!i.isloading]]),d(t("div",M,R,512),[[r,i.isloading]])])}const U=f(N,[["render",G],["__scopeId","data-v-364edd41"]]);export{U as default};
