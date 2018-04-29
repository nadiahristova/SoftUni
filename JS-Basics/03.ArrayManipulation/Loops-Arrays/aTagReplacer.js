function replaceATag(str){
	var indexOfFirstTag;
	while (str.indexOf('<a')!==-1) {indexOfFirstTag= str.indexOf('<a');
    str = str.replace("<a", "[URL");
    str = str.substring(0, indexOfFirstTag) + str.substring(indexOfFirstTag).replace(">", "]");  
    str = str.replace('</a>', '[/URL]');}
    console.log(typeof str);
    console.log(str);
}

replaceATag('<ul>\n <li>\n  <a href=http://softuni.bg>SoftUni</a>\n <a> </a></li>\n</ul>');