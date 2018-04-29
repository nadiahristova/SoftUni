createParagraph('wrapper', 'Some text')

function createParagraph(id, text){
	var p = document.createElement('p');
	p.innerText = text;
	var dFrag = document.createDocumentFragment();
	dFrag.appendChild(p);
	var parent = document.getElementById(id);
	parent.appendChild(dFrag);
}