package exam5;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class _03_ValidUsernames {
	public static void main(String[] args) {
		String str = "ds3bhj y1ter/wfsdg 1nh_jgf ds2c_vbg\\4htref";
		String[] usernames = str.split("[ \\\\/()]+");
		String regex = "\\b[a-zA-z][\\p{Alnum}_]{2,25}\\b";
		Pattern pattern = Pattern.compile(regex);
		String beforeWord = "";
		String currWord = "";
		int maxLength = 0;
		for (int i = 0; i < usernames.length - 1; i++) {			
			Matcher match1 = pattern.matcher(usernames[i]);
			for (int k = i+1; k < usernames.length; k++) {
				Matcher match2 = pattern.matcher(usernames[k]);
				if (match1.find() && match2.find()) {
					int length = usernames[i].length() + usernames[k].length();
					if (length > maxLength) {
						beforeWord = usernames[i];
						currWord = usernames[k];
						maxLength = length;
						break;
					}
			}			
			}
		}
		System.out.println(beforeWord + "\n" + currWord + "\n");
	}
}
