var HTMLGen = {
	createParagraph: function(id, text){
		var p = document.createElement('p');
		p.innerText = text;
		var dFrag = document.createDocumentFragment();
		dFrag.appendChild(p);
		var parent = document.getElementById(id);
		parent.appendChild(dFrag);
	},	
	createDiv:function(id, className){
		var div = document.createElement('div');
		div.className = className;	
		var parent = document.getElementById(id);
		parent.appendChild(div);
	},
	createLink: function(id, shownMsg, link){
		var a = document.createElement('a');
		a.innerText = shownMsg;
		a.href = link;
		var parent = document.getElementById(id);
		parent.appendChild(a);
	}
}

HTMLGen.createParagraph('wrapper', 'Soft Uni');
HTMLGen.createDiv('wrapper', 'section');
HTMLGen.createLink('book', 'C# basics book', 'http://www.introprogramming.info/');
