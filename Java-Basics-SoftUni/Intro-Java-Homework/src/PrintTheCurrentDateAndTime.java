import java.util.Date;
import java.text.*;

public class PrintTheCurrentDateAndTime {
	public static void main(String[] args) {
		Date today = new Date();
		System.out.println(today.toString());
		
		SimpleDateFormat form = new SimpleDateFormat ("E yyyy.MM.dd 'at' hh:mm:ss a zzz");
		System.out.println("Current Date: " + form.format(today));
		
		String str = String.format("Curent Date/Time: %tc", today);
		System.out.printf(str);
		System.out.println();
		
		System.out.printf("%1$s %2$tB %2$td, %2$tY", "Due date: ", today);
		System.out.println();		
		System.out.printf("%s %tB %<te, %<tY", "Due date: ", today);
	} 
}
