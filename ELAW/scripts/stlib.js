/*=======Ver:2.40.80310========*/
/*TreeMenu, (c) 2007, SourceTec Software Co.,LTD  -  www.sothink.com*/
if(typeof _STJS=="undefined"){if(!Array.prototype.push){Array.prototype.push=function(){for(var i=0;i<arguments.length;i++){this[this.length]=arguments[i];}};}if(!Array.prototype.pop){Array.prototype.pop=function(){if(this.length){var o=this[this.length-1];this.length--;return o;}};}_STJS={version:"2.1",build:"070321",releasePath:"",debugPath:"",isDebug:false,outWin:null,url:location.href+"",folder:"",trace:function(s){if(_STJS.outWin){_STJS.$(output,outWin).value+=s;}else{alert(s);}},isBuffImg:true,navigator:null,isIE:false,isFX:false,isSF:false,isOP:false,isMIE:false,dMode:null,checkDocumentMode:function(){switch(document.compatMode){case "QuirksMode":return "quirks";case "BackCompat":return "back";case "CSS1Compat":return "css1";default:return "unknow";}},getNavigator:function(){var _n=navigator,_u=_n.userAgent,_a=_n.appName,_p=_n.platform,n,v,p;if(/(Opera)[ \/]([\d\.]+)/.test(_u)||/(Netscape)\d*\/([\d\.]+)/.test(_u)||/(MSIE) ([\d\.]+)/.test(_u)||/(Safari)\/([\d\.]+)/.test(_u)||/(Konqueror)\/([\d\.]+)/.test(_u)||/(Gecko)\/(\d+)/.test(_u)){n=RegExp.$1.toLowerCase();v=RegExp.$2;}else{if(_a=="Netscape"&&_n.appVersion.charAt(0)=="4"){n="netscape4";v=parseFloat(_n.appVersion);}else{n="unknow";v=0;}}if(n=="netscape"){switch(_a){case "Microsoft Internet Explorer":n="msie";v=/(MSIE) ([\d\.]+)/.exec(_u)[2];break;case "Netscape":n="gecko";v=/(Gecko)\/(\d+)/.exec(_u)[2];}}if(/^(Win)/.test(_p)||/^(Mac)/.test(_p)||/^(SunOS)/.test(_p)||/^(Linux)/.test(_p)||/^(Unix)/.test(_p)){p=RegExp.$1.toLowerCase();}else{p=_p;}return {appName:n,appVersion:v,platform:p};},createElement:function(_b,w){if(w==null){w=window;}return w.document.createElement(_b);},getElementById:function(id,w){if(w==null){w=window;}with(_STJS){if(isIE){var es=w.document.all(id);if(es&&es.length){return es[0];}else{return es;}}else{return w.document.getElementById(id);}}},getElementsByTagName:function(n,w){if(w==null){w=window;}with(_STJS){if(isIE){return w.document.all.tags(n);}else{return w.document.getElementsByTagName(n);}}},getElementsByClassName:function(n,w){if(w==null){w=window;}var i,a=[],el,els=_STJS.isIE?w.document.all:w.document.getElementsByTagName("*");for(i=0;el=els[i];i++){if(el.className==n){a.push(el);}}return a;},insertAdjacentHTML:function(o,w,h){if(_STJS.isIE){return o.insertAdjacentHTML(w,h);}else{var t=document.createElement("span");t.innerHTML=h;switch(w){case "beforeBegin":return o.parentNode.insertBefore(t,o);case "afterBegin":return o.insertBefore(t,o.firstChild);case "beforeEnd":return o.appendChild(t);case "afterEnd":if(o.nextSibling){return o.parentNode.insertBefore(t,o.nextSibling);}else{return o.parentNode.appendChild(t);}}}if(_STJS.isDebug){_STJS.trace("Insert HTML error!");}return false;},readCookieByName:function(n){var i,cs=document.cookie.split("; ");for(i=0;i<cs.length;i++){if(!cs[i].indexOf("sothink_"+n+"=")){return cs[i].substr(n.length+9);}}},saveCookie:function(n,v,t){var s="sothink_"+n+"="+v+"; ",d=new Date;if(!t||!v){s+="expires=Fri, 31 Dec 1999 23:59:59 GMT; ";}else{s+="expires="+((new Date(d-0+t)).toGMTString())+"; ";}s+="path=/; ";document.cookie=s;},shielded:false,cssShield:{tb:"border-style:none;background-color:transparent;background-image:none;",tr:"border-style:none;background-color:transparent;background-image:none;",td:"border-style:none;background-color:transparent;background-image:none;",dv:"margin:0px;padding:0px;background-color:transparent;background-image:none;",a:"border-style:none;margin:0px;padding:0px;background-color:transparent;background-image:none;"},styleShield:function(){with(_STJS){if($T("body")&&$T("body").length){shielded=false;}else{var i,s="<style>";for(i in cssShield){if(i=="a"){s+=".sta:link,.sta:hover,.sta:active,.sta:visited";}else{s+=".st"+i;}s+="{"+cssShield[i]+"}";}s+="</style>";shielded=true;document.write(s);}}},loads:[],loaded:false,setLoad:function(){if(_STJS.isIE&&window.attachEvent){window.attachEvent("onload",_STJS.onLoad);}else{if(_STJS.navigator.appName!="konqueror"&&window.addEventListener){window.addEventListener("load",_STJS.onLoad,false);}else{if(!window.onload||onload.toString()!=_STJS.onLoad.toString()){if(typeof (onload)=="function"){_STJS.loads.push(onload);}onload=_STJS.onLoad;}}}},onLoad:function(){with(_STJS){if(loaded){return;}loaded=true;for(var j=0;j<loads.length;j++){loads[j]();}}},checks:[],checkPage:function(){if(_STJS.checks.length){for(var i=0;i<_STJS.checks.length;i++){_STJS.checks[i]();}setTimeout("_STJS.checkPage()",1000);}},jsPath:"",getJsPath:function(){var ss=_STJS.$T("script"),s;if(ss.length){s=ss[ss.length-1].src;if(s){return s.substr(0,s.lastIndexOf("/")+1)+(_STJS.isDebug?_STJS.debugPath:_STJS.releasePath);}}return "";},structs:[],_cs:[],getStructByName:function(n){for(var i=0;i<_STJS.structs.length;i++){if(_STJS.structs[i].uid==n){return _STJS.structs[i];}}},mods:{},registerMod:function(s,d){with(_STJS){if(mods[s]){return mods[s].state;}else{mods[s]={state:1,defer:d};}return 1;}},completeMod:function(s,o){with(_STJS){if(mods[s]){mods[s].state=3;mods[s].obj=o;}else{return false;}}},loadMod:function(){var i,s="",t;with(_STJS){for(i in mods){if(mods[i].state&&mods[i].state!=3){mods[i].state=2;s+=getScriptTag(isABSPath(i)?i:jsPath+i,mods[i].defer);}}write(s);}},write:function(s){with(_STJS){if(s){if(loaded){insertAdjacentHTML(document.body,"beforeEnd",s);}else{document.write(s);}}}},getScriptTag:function(s,f){return "<script type='text/javascript' language='javascript1.2' src=\""+s+"\""+(f?" DEFER":"")+"></"+"script>";},getImgTag:function(s,w,h,b,id){if(!w||!h){return "";}return "<img class='stimg' src=\""+s+"\""+(w==-1?"":" width="+w)+(h==-1?"":" height="+h)+" border="+(b?b:0)+(id?" id='"+id+"'":"")+"/>";},getTag:function(t,a,s){return "<"+t+" "+a+">"+s+"</"+t+">";},isFile:function(s){return /\w+\.\w+$/.test(s);},isImg:function(s){return /\.(gif|png|jpg|jpeg|bmp)$/.test(s.toLowerCase());},isABSPath:function(s){if(!s){return true;}var t=s.toLowerCase();return /^(#|\?|\/|[a-z]:|http:|https:|file:|ftp:|javascript:|mailto:|about:|gopher:|news:|res:|telnet:|view-source|wais:|rtsp:|mms:)/.test(t);},getFolder:function(s){var ts=s.toLowerCase();if(!ts.indexOf("//")||!ts.indexOf("file:/")||!ts.indexOf("http://")||!ts.indexOf("https://")){return s.substr(0,s.lastIndexOf("/")+1);}else{return "";}},getABS:function(s){if(!s){return s;}var re,t;if(s.charAt(0)=="/"){re=/(file:\/{2,}[^\/]+\/|http:\/\/[^\/]+\/|https:\/\/[^\/]+\/)/;if(re.exec(_STJS.folder)){s=RegExp.$1+s.substr(1);}}else{if(s=="#"){return this.url+"#";}else{if(!_STJS.isABSPath(s)){s=_STJS.folder.substr(0,_STJS.folder.lastIndexOf("/")+1)+s;}else{return s;}}}while(s.indexOf("/./")>0){s=s.replace("/./","/");}while((t=s.indexOf("/../"))>0){var p1,p2;p1=s.substr(0,t);p2=s.substr(t).replace("/../","");p1=p1.substr(0,p1.lastIndexOf("/")+1);s=p1+p2;}return s;},cssLen:function(t,l,p,b,s,w){var _r=_STJS;if(w==null){w=true;}if(b==null){b=0;}switch(t){case "dv":if(_r.dMode=="css1"||(!_r.isIE&&!_r.isOP||(_r.isOP&&parseInt(_r.navigator.appVersion)>=8))){return l-2*p-2*b;}break;case "tb":if(_r.isMIE&&!w&&s){return l-2*b-2*p;}break;case "td":if(_r.isSF){if(w){if(s){return l-2*b-2*p;}else{return l-2*p;}}else{if(_r.dMode!="css1"){return l-2*p-2*b;}}}else{if(!_r.isMIE&&(_r.isIE&&_r.dMode=="css1"||w)){return l-2*b-2*p;}}break;}return l;},htmlCode:function(s,f){if(s){s=s.replace(/&/g,"&amp;");if(!f){s=s.replace(/ /g,"&nbsp;");}s=s.replace(/</g,"&lt;");s=s.replace(/>/g,"&gt;");s=s.replace(/\r\n/g,"<br />");s=s.replace(/"/g,"&quot;");return s;}else{return "";}},joinA:function(a,b){for(var i=0;i<b.length;i++){if(a[i]==null){a[i]=b[i];}}},UIObj:function(){var _r=_STJS;this.getMsg=_r.getMsg;this.setMsg=_r.setMsg;this.attachEvent=_r.attachUIEvent;this.detachEvent=_r.detachUIEvent;this.fire=_r.fire;this.setA=_r.setAttributeFromHashArray;},setAttributeFromHashArray:function(n,a,id){if(typeof a[n]!="undefined"){this[n]=a[n];}else{if(typeof a[id]!="undefined"){this[n]=a[id];}else{if(typeof this[n]=="undefined"){this[n]=null;}}}},getMsg:function(m,d){var f,r=true;if(this._ms[m]){if(typeof this._ms[m]=="string"){if(f=_STJS.getFun(this._ms[m])){if(typeof f=="function"){r=f(this,d);}else{if(typeof f=="boolean"){r=f;}}}}else{if(typeof this._ms[m]=="function"){r=this._ms[m](this,d);}}}if(r==true&&this.offsetPar){this.offsetPar.getMsg(m,d);}},ILINK:false,ILOC:false,isLink:function(h,tar,w,c){var t=_STJS.getWindow(tar,w);if(!t){return false;}var h=_STJS.getABS(h);var u=t.location.href;if(!c){u=u.toLowerCase();h=h.toLowerCase();}return u&&h&&(u==h||u==h+"/"||u==h+"#"||_STJS.ILINK&&u==h.substr(0,Math.max(0,h.indexOf("?")))||_STJS.ILOC&&h==u.substr(0,Math.max(0,u.indexOf("?"))));},getWindow:function(t,w){if(t=="_self"){return w;}else{if(t=="_parent"){return w.parent;}else{if(t=="_top"){return w.top;}else{if(w.frames[t]){return w.frames[t];}else{return w.parent.frames[t];}}}}return 0;},setMsg:function(m,f){this._ms[m]=f;},fire:function(t){var _t=this,i,r=true,f;if(!_t._es[t]||!_t._es[t].length){return -1;}for(i=0;i<_t._es[t].length;i++){if(!_t._es[t][i]){continue;}if(typeof _t._es[t][i]=="function"&&!_t._es[t][i](_t)){r=false;}else{if(f=_STJS.getFun(_t._es[t][i])){if(typeof f=="function"&&!f(_t)){r=false;}}else{if(f==false){r=false;}}}}return r;},attachUIEvent:function(t,f){var _t=this,i;if(typeof f!="string"&&typeof f!="function"){if(_STJS.isDebug){_STJS.trace("Attach event error!");}return false;}if(_t._es[t]){if(typeof f=="string"){for(i=0;i<_t._es[t].length;i++){if(_t._es[t][i]==f){return i;}}}_t._es[t].push(f);}else{_t._es[t]=[f];}return _t._es[t].length-1;},detachUIEvent:function(t,f){var i,k=0,_t=this;if(typeof f!="string"&&typeof f!="number"){if(_STJS.isDebug){_STJS.trace("Detach event error!");}return false;}if(_t._es[t]&&_t._es[t].length){if(typeof f=="number"&&f<_t._es[t].length){_t._es[t][f]=null;}else{for(i=0;i<_t._es[t].length;i++){if(k){_t._es[t][i-1]=_t._es[t][i];}else{if(_t._es[t][i]==f){k=1;}}}if(k){_t._es[t].length--;}}}},getFun:function(n){if(!n){if(_STJS.isDebug){_STJS.trace("Null function!");}return -1;}if(typeof n=="function"){return n;}else{if(typeof n=="string"){return eval(n);}}},domEvent:function(e,o){var oid=id=o.id;if(!oid){return true;}var ob,ids=oid.split("_"),r=-1,i,j;with(_STJS){if(ob=getObjById(oid)){if(!ob.getEvent){return true;}switch(e.type){case "mouseover":if(!o._ov&&isIE&&e.toElement&&o.contains(e.toElement)){o._ov=1;r=ob.getEvent(e,oid);}else{if(!isIE&&!o._ov&&!isParent(o,e.relatedTarget)){o._ov=1;r=ob.getEvent(e,oid);}else{return;}}if(!r){return;}break;case "mouseout":if(o._ov&&isIE&&(!e.toElement||!o.contains(e.toElement))){o._ov=0;r=ob.getEvent(e,oid);}else{if(!isIE&&o._ov&&(!e.relatedTarget||!isParent(o,e.relatedTarget))){o._ov=0;r=ob.getEvent(e,oid);}else{return;}}if(!r){return;}break;default:r=ob.getEvent(e,oid);}}if(typeof r=="boolean"){e.cancelBubble=!r;return r;}}return true;},os:{},getObjById:function(id){var tid=id,o;while(tid){if(o=_STJS.os[tid]){return o;}tid=tid.substr(0,tid.lastIndexOf("_"));}},addObjById:function(id,o){_STJS.os[id]=o;},delObjById:function(id){if(_STJS.os[id]){delete _STJS.os[id];}},isParent:function(p,c){if(!p||!c){return false;}if(p==c){return true;}do{if(c.parentNode){c=c.parentNode;}else{return false;}if(p==c){return true;}}while(c);return false;},S64:"0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ#@",transXto10:function(n,m){m=String(m).replace(/ /gi,"");if(m==""){return 0;}var a=_STJS.S64.substr(0,n);if(eval("m.replace(/["+a+"]/gi,'')")!=""&&_STJS.isDebug){_STJS.trace("Transform bad number from "+x+" to 10!");return 0;}var t=0,c=1;for(var x=m.length-1;x>-1;x--){t+=c*(a.indexOf(m.charAt(x)));c*=n;}return t;},trans10toX:function(n,m){m=String(m).replace(/ /gi,"");if(m==""){return 0;}if(parseInt(m)!=m){_STJS.trace("Transform bad number from 10 to "+x+"!");return 0;}var t="",a=_STJS.S64.substr(0,n);while(m!=0){var b=m%n;t=a.charAt(b)+t;m=(m-b)/n;}if(!t){t="0";}return t;},output:"",exec:function(){if(_STJS.output){eval(_STJS.output);_STJS.output="";}}};stGetMessage=function(s,a,d){var _r=_STJS,as=a.split(","),i,o;for(i=0;i<as.length;i++){if(o=_r.os[as[i]]){o.getMsg(s,d);}}};stSendMessage=function(s){location.assign("DMM:"+s);};stParseXML=function(s){};stBM=function(t,id,a){var lib="sttree.js";with(_STJS){registerMod(lib);_cs[id]={tag:"tree",id:id,as:a};_cs.push(_cs[id]);output="_STJS.mods['"+lib+"'].obj.create(_STJS._cs)";}return a;};stEM=function(){with(_STJS){_cs.push({tag:"/tree"});loadMod();document.write(getScriptTag(jsPath+"stapp.js"));}};stBS=function(id,a,pid){with(_STJS){if(pid&&_cs[pid]){joinA(a,_cs[pid].as);}_cs[id]={tag:"subtree",id:id,as:a};_cs.push(_cs[id]);}return a;};stES=function(){_STJS._cs.push({tag:"/subtree"});};stIT=function(id,a,pid){with(_STJS){if(pid&&_cs[pid]){joinA(a,_cs[pid].as);}_cs[id]={tag:"node",id:id,as:a};_cs.push(_cs[id]);}return a;};stExpandSubTree=function(n,s,r){var _r=_STJS,t=_r.getStructByName(n),st;if(t){if(st=_r.os[n+t.nid+"_"+s]){st.getMsg("ST_EXPAND",r);}}};stCollapseSubTree=function(n,s,r){var _r=_STJS,t=_r.getStructByName(n),st;if(t){if(st=_r.os[n+t.nid+"_"+s]){st.getMsg("ST_COLLAPSE",r);}}};stGetNodesByText=function(n,tx,m){var _r=_STJS,t=_r.getStructByName(n),ns=[],i,j,tmp,ts;if(t){for(i=0;i<t.subTrees.length;i++){for(j=0;j<t.subTrees[i].nodes.length;j++){if(t.subTrees[i].nodes[j].text){if(m){tmp=t.subTrees[i].nodes[j].text.toLowerCase();ts=tx.toLowerCase();if(tmp.indexOf(ts)!=-1){ns.push(t.subTrees[i].nodes[j]);}}else{if(t.subTrees[i].nodes[j].text==tx){ns.push(t.subTrees[i].nodes[j]);}}}}}}return ns;};stGetNodesByLink=function(n,l,m){var _r=_STJS,t=_r.getStructByName(n),ns=[],i,j,tmp,ls;if(t){for(i=0;i<t.subTrees.length;i++){for(j=0;j<t.subTrees[i].nodes.length;j++){if(t.subTrees[i].nodes[j].link&&t.subTrees[i].nodes[j].link!="#_nolink"){if(m){tmp=_r.getABS(t.subTrees[i].nodes[j].link).toLowerCase();ls=l.toLowerCase();if(tmp.indexOf(ls)!=-1){ns.push(t.subTrees[i].nodes[j]);}}else{if(t.subTrees[i].nodes[j].link==l){ns.push(t.subTrees[i].nodes[j]);}}}}}}return ns;};with(_STJS){_STJS.$=getElementById;_STJS.$T=getElementsByTagName;_STJS.$C=createElement;navigator=getNavigator();isIE=navigator.appName=="msie";isOP=navigator.appName=="opera";isFX=navigator.appName=="gecko";isMIE=isIE&&navigator.platform=="mac";isSF=navigator.appName=="safari";folder=getFolder(url);dMode=checkDocumentMode();jsPath=getJsPath();loads.push(_STJS.checkPage);setLoad();styleShield();}}