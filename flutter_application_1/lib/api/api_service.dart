import 'package:http/http.dart' as http;
import 'dart:convert';
import '../model/login_model.dart';

/* class APIService {
  Future<LoginResponseModel> login(LoginRequestModel requestModel) async {
    // Remove the unused variable 'url'
    // String url = "https://reqres.in/api/login";

    final response = await http.post(Uri.parse("https://reqres.in/api/login"),
        body: requestModel.toJson());
    if (response.statusCode == 200 || response.statusCode == 400) {
      return LoginResponseModel.fromJson(
          json.decode(response.body),
          // ignore: avoid_print
          print(response.body));
    } else {
      // ignore: avoid_print
      print(response.body);

      throw Exception('Failed to load data!');
    }
  }
}
 */

class APIService {
  Future<bool> loginUser(String email, String password) async {
    final response = await http.post(
      Uri.parse('http://10.0.2.2:7285/api/User/authenticate'),
     //  Uri.parse('http://api/User/authenticate'),
        // Uri.parse('https://reqres.in/api/login'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'Email': email,
        'Password': password,
       /*   'email': email,
         'password': password, */
      }),
    );

    if (response.statusCode == 200) {
      // If the server returns a 200 OK response,
      // then parse the JSON.
      return true;
    } else {
      // If the server returns an unexpected response,
      // then throw an exception.
      return false;
    }
  }
}
