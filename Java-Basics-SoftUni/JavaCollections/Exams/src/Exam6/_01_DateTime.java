package Exam6;

import java.time.Duration;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.Scanner;

public class _01_DateTime {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		DateTimeFormatter formatt = DateTimeFormatter.ofPattern("H:mm:ss");
		LocalTime firs = LocalTime.parse(input.nextLine().trim(),formatt);
		LocalTime second = LocalTime.parse(input.nextLine().trim(),formatt);
		long secs = Duration.between(second, firs).toMillis()/1000;	
		System.out.printf("%d:%02d:%02d",secs/3600, (secs%3600)/60,secs%60);		 
	}

}
