import{_ as k,f as C,r as w,o as d,c as u,d as o,w as h,v as g,I as N,D as f,F as _,e as b,i as I,E as y,t as m,a as S,h as D,H as A,p as T,g as M,A as x}from"./index-44f62c83.js";import{_ as E}from"./Spinner-1s-200px-2-0335de7c.js";const B={mixins:[C],data(){return{input:{activityName:"",categoryId:0,address:"",time:new Date,memberId:0},result:[],minDate:new Date,categoryOption:[],isloading:!0,isempty:!0}},mounted(){this.getMemberId(),this.getCategory(),this.setTime(),this.fetchActivityData(),this.$nextTick(()=>{this.result.forEach(e=>{this.$el.querySelector(".addressTag").addEventListener("click",()=>{this.Citytag(e)})})}),this.$nextTick(()=>{this.result.forEach(e=>{this.$el.querySelector(".categoryTag").addEventListener("click",()=>{this.Categorytag(e)})})})},methods:{async fetchActivityData(){this.isloading=!0,this.isempty=!1;let e=await this.getMemberId(),t={activityName:this.input.activityName,categoryId:this.input.categoryId,address:this.input.address,time:this.input.time,memberId:e};console.log(t);let i="https://localhost:7259/api/Activity/Search?";t.activityName!==""&&(i+=`activityName=${t.activityName}&`),t.categoryId!==0&&(i+=`categoryId=${t.categoryId}&`),t.address!==""&&(i+=`address=${t.address}&`),i+=`time=${t.time}&`,i+=`memberId=${t.memberId}`,console.log(i),this.isempty=!1;const s=await(await fetch(i)).json();this.isloading=!1,this.result=s,s.length==0&&(this.isempty=!0)},setTime(){let e=new Date,t={year:e.getFullYear(),month:e.getMonth()+1,day:e.getDate(),hour:e.getHours(),min:e.getMinutes()};t.month<10&&(t.month=`0${t.month}`),t.day<10&&(t.day=`0${t.day}`),t.hour<10&&(t.hour=`0${t.hour}`),t.min<10&&(t.min=`0${t.min}`);let i=`${t.year}-${t.month}-${t.day}T${t.hour}:${t.min}`;this.input.time=i,this.minDate=i},Citytag(e){this.input.address=e,this.input.activityName="",this.input.categoryId=0,this.setTime(),this.fetchActivityData()},Categorytag(e){this.input.address="",this.input.activityName="",this.input.categoryId=e,this.setTime(),this.fetchActivityData()},async getMemberId(){let e=0,i={method:"GET",headers:{Authorization:`Bearer ${$.cookie("token")}`}};try{let s=await(await fetch("https://localhost:7259/api/Members/Read",i)).json();return console.log(s),e=s,this.input.memberId=e,e}catch{return console.log("未登入"),this.input.memberId=e,e}},async getCategory(){let t=await(await fetch("https://localhost:7259/api/Activity/Category")).json();this.categoryOption=t},save(e,t,i,r){e.stopPropagation();let s;e.target.tagName.toLowerCase()==="button"?s=e.target:e.target.tagName.toLowerCase()==="i"&&(s=e.currentTarget),this.result[t].numOfCollections++,s.innerHTML=`<i style="width: 16px;
        color: #444;
        margin-right: 10px;"
        class="fa-solid fa-bookmark"></i>`,fetch("https://localhost:7259/api/ActivitySave/Save",{method:"POST",headers:{"Content-Type":"application/json"},body:JSON.stringify({memberId:r,activityId:i})}).then(c=>c.json()).then(c=>{console.log("Success:",c),c.result?(this.result[t].statusId=4,this.result[t].unSaveId=c.activityCollectionId,this.showAlert(c.message)):this.showAlert(c.message)}).catch(c=>{console.error("Error:",c)})},unsave(e,t,i){e.stopPropagation(),this.result[t].numOfCollections--;let r;e.target.tagName.toLowerCase()==="button"?r=e.target:e.target.tagName.toLowerCase()==="i"&&(r=e.currentTarget),r.innerHTML=`<i style="width: 16px;color: #444444;margin-right: 10px;" 
                                class="fa-regular fa-bookmark"></>`,fetch("https://localhost:7259/api/ActivitySave/UnSave/"+i,{method:"Delete"}).then(s=>s.json()).then(s=>{console.log("Success:",s),s.result?(this.result[t].statusId=3,this.result[t].unSaveId=0,this.showAlert(s.message)):this.showAlert(s.message)}).catch(s=>{console.error("Error:",s)})}}},l=e=>(T("data-v-acf80baa"),e=e(),M(),e),L={class:"container"},O={class:"searchPage"},V=l(()=>o("h4",{class:"align-self-center mt-4"},"活動總覽",-1)),j={class:"row inputSearch"},P={class:"col-lg-3 col-md-4 col-12"},U={class:"col-lg-2 col-md-4 col-6"},q=l(()=>o("option",{value:"",disabled:""},"選擇拍攝類型",-1)),H=l(()=>o("option",{value:"0"},"所有拍攝類型",-1)),F=["value"],K={class:"col-lg-2 col-md-4 col-6"},z=x('<option disabled data-v-acf80baa>選擇地區</option><option value="" selected data-v-acf80baa>所有地區</option><option value="基隆市" data-v-acf80baa>基隆市</option><option value="台北市" data-v-acf80baa>台北市</option><option value="新北市" data-v-acf80baa>新北市</option><option value="桃園市" data-v-acf80baa>桃園市</option><option value="新竹市" data-v-acf80baa>新竹市</option><option value="新竹縣" data-v-acf80baa>新竹縣</option><option value="苗栗縣" data-v-acf80baa>苗栗線</option><option value="台中市" data-v-acf80baa>台中市</option><option value="彰化縣" data-v-acf80baa>彰化縣</option><option value="南投縣" data-v-acf80baa>南投縣</option><option value="雲林縣" data-v-acf80baa>雲林縣</option><option value="嘉義市" data-v-acf80baa>嘉義市</option><option value="嘉義縣" data-v-acf80baa>嘉義縣</option><option value="台南市" data-v-acf80baa>台南市</option><option value="高雄市" data-v-acf80baa>高雄市</option><option value="屏東縣" data-v-acf80baa>屏東縣</option><option value="宜蘭縣" data-v-acf80baa>宜蘭縣</option><option value="花蓮縣" data-v-acf80baa>花蓮縣</option><option value="台東縣" data-v-acf80baa>台東縣</option><option value="澎湖縣" data-v-acf80baa>澎湖縣</option><option value="金門縣" data-v-acf80baa>金門縣</option><option value="連江縣" data-v-acf80baa>連江縣</option>',24),G=[z],J={class:"col-lg-5 col-md-12 col-12"},R=["min"],Y={class:"row",id:"resultCard"},Q=["activityId"],W={class:"card-header"},X=["src"],Z={class:"card-body"},tt=l(()=>o("i",{class:"fa-solid fa-calendar-days"},null,-1)),et=l(()=>o("i",{class:"fa-solid fa-map-pin"},null,-1)),at=["onClick","city"],ot={class:"tag"},st=["onClick","categoryId"],it={class:"info"},nt={class:"enroll"},ct=l(()=>o("i",{class:"fa-solid fa-user"},null,-1)),lt={class:"num"},rt={class:"save"},dt=["activityId"],ut=l(()=>o("i",{class:"fa-regular fa-bookmark"},null,-1)),ht=[ut],mt=["onClick"],pt=l(()=>o("i",{class:"fa-regular fa-bookmark"},null,-1)),vt=[pt],yt=["onClick"],gt=l(()=>o("i",{class:"fa-solid fa-bookmark"},null,-1)),ft=[gt],_t={class:"num"},bt={class:"nothingPage"},It={class:"image-container"},kt=l(()=>o("img",{src:E,alt:""},null,-1)),Ct=[kt];function wt(e,t,i,r,s,n){const c=w("router-link");return d(),u("div",L,[o("div",O,[V,o("form",j,[o("div",P,[h(o("input",{"onUpdate:modelValue":t[0]||(t[0]=a=>s.input.activityName=a),onKeydown:t[1]||(t[1]=N((...a)=>n.fetchActivityData&&n.fetchActivityData(...a),["enter"])),class:"inputName",name:"activityName",id:"activityName",type:"text",placeholder:"輸入活動名稱關鍵字...",autocomplete:"off"},null,544),[[g,s.input.activityName]])]),o("div",U,[h(o("select",{onChange:t[2]||(t[2]=(...a)=>n.fetchActivityData&&n.fetchActivityData(...a)),"onUpdate:modelValue":t[3]||(t[3]=a=>s.input.categoryId=a),class:"inputCategory",name:"categoryId",id:"categoryId"},[q,H,(d(!0),u(_,null,b(s.categoryOption,a=>(d(),u("option",{key:a.categoryName,value:a.id},m(a.categoryName),9,F))),128))],544),[[f,s.input.categoryId]])]),o("div",K,[h(o("select",{onChange:t[4]||(t[4]=(...a)=>n.fetchActivityData&&n.fetchActivityData(...a)),"onUpdate:modelValue":t[5]||(t[5]=a=>s.input.address=a),class:"inputAddress",name:"address",id:"adress"},G,544),[[f,s.input.address]])]),o("div",J,[h(o("input",{onChange:t[6]||(t[6]=(...a)=>n.fetchActivityData&&n.fetchActivityData(...a)),"onUpdate:modelValue":t[7]||(t[7]=a=>s.input.time=a),min:s.minDate,class:"inputTime",name:"time",type:"datetime-local",id:"inputTime"},null,40,R),[[g,s.input.time]]),I(" 之後的活動 ")])]),h(o("div",Y,[(d(!0),u(_,null,b(s.result,(a,v)=>(d(),u("div",{key:v,class:"col-12 col-sm-6 col-md-4 col-xl-3"},[o("div",{class:"card",activityId:a.activityId},[o("div",W,[S(c,{to:"/Activity/"+a.activityId},{default:D(()=>[o("img",{src:a.coverImage,alt:"cover"},null,8,X)]),_:2},1032,["to"])]),o("div",Z,[o("h5",null,m(a.activityName),1),o("p",null,[tt,I(m(a.gatheringTime),1)]),o("p",null,[et,o("a",{onClick:p=>n.Citytag(a.city),class:"addressTag",city:a.city},m(a.city),9,at)]),o("span",ot,[o("a",{onClick:p=>n.Categorytag(a.categoryId),class:"categoryTag",categoryId:a.categoryId},m(a.categoryName),9,st)])]),o("div",it,[o("div",nt,[ct,o("p",lt,m(a.numOfEnrolment),1)]),o("div",rt,[this.input.memberId==0?(d(),u("button",{key:0,"data-bs-toggle":"modal","data-bs-target":"#loginModal",type:"button",class:"saveBtn1",activityId:a.activityId},ht,8,dt)):a.statusId==3&&this.input.memberId!=0?(d(),u("button",{key:1,onClick:p=>n.save(p,v,a.activityId,this.input.memberId),type:"button",class:"saveBtn"},vt,8,mt)):a.statusId==4&&this.input.memberId!=0?(d(),u("button",{key:2,onClick:p=>n.unsave(p,v,a.unSaveId),type:"button",class:"unsaveBtn"},ft,8,yt)):A("",!0),o("p",_t,m(a.numOfCollections),1)])])],8,Q)]))),128))],512),[[y,!s.isempty&&!s.isloading]]),h(o("div",bt,"找不到符合的資料",512),[[y,s.isempty]]),h(o("div",It,Ct,512),[[y,s.isloading]])])])}const Dt=k(B,[["render",wt],["__scopeId","data-v-acf80baa"]]);export{Dt as default};
