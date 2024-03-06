class LoginResponseModel {
  final String token;
  final String error;

  LoginResponseModel({required this.token, required this.error});

  factory LoginResponseModel.fromJson(Map<String, dynamic> json, void print) {
    return LoginResponseModel(
      token: json["token"] ?? "",
      error: json["error"] ?? "",
    );
  }
}

class LoginRequestModel {
   String email;
  String password;

  LoginRequestModel({
    required this.email,
    required this.password,
  });

  Map<String, dynamic> toJson() {
    Map<String, dynamic> map = {
      'email': email.trim(),
      'password': password.trim(),
    };

    return map;
  } 
 /* String _email; // Private variable to hold the email value
  String _password; // Private variable to hold the password value

  LoginRequestModel({
    required String email,
    required String password,
  })  : _email = email,
        _password = password;

  // Getter method for email
  String get email => _email;

  // Setter method for email
  set email(String value) {
    _email = value;
  }

  // Getter method for password
  String get password => _password;

  // Setter method for password
  set password(String value) {
    _password = value;
  }

  // Convert LoginRequestModel to JSON format
  Map<String, dynamic> toJson() {
    return {
      'email': _email.trim(),
      'password': _password.trim(),
    };
  }*/
}
