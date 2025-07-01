// GENERATED CODE - DO NOT MODIFY BY HAND
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'intl/messages_all.dart';

// **************************************************************************
// Generator: Flutter Intl IDE plugin
// Made by Localizely
// **************************************************************************

// ignore_for_file: non_constant_identifier_names, lines_longer_than_80_chars
// ignore_for_file: join_return_with_assignment, prefer_final_in_for_each
// ignore_for_file: avoid_redundant_argument_values, avoid_escaping_inner_quotes

class S {
  S();

  static S? _current;

  static S get current {
    assert(
      _current != null,
      'No instance of S was loaded. Try to initialize the S delegate before accessing S.current.',
    );
    return _current!;
  }

  static const AppLocalizationDelegate delegate = AppLocalizationDelegate();

  static Future<S> load(Locale locale) {
    final name =
        (locale.countryCode?.isEmpty ?? false)
            ? locale.languageCode
            : locale.toString();
    final localeName = Intl.canonicalizedLocale(name);
    return initializeMessages(localeName).then((_) {
      Intl.defaultLocale = localeName;
      final instance = S();
      S._current = instance;

      return instance;
    });
  }

  static S of(BuildContext context) {
    final instance = S.maybeOf(context);
    assert(
      instance != null,
      'No instance of S present in the widget tree. Did you add S.delegate in localizationsDelegates?',
    );
    return instance!;
  }

  static S? maybeOf(BuildContext context) {
    return Localizations.of<S>(context, S);
  }

  /// `Your Health, Smarter`
  String get your_health_smarter {
    return Intl.message(
      'Your Health, Smarter',
      name: 'your_health_smarter',
      desc: '',
      args: [],
    );
  }

  /// `Connect with your doctor, track your condition, and get care from anywhere.`
  String get connect_doctor_track {
    return Intl.message(
      'Connect with your doctor, track your condition, and get care from anywhere.',
      name: 'connect_doctor_track',
      desc: '',
      args: [],
    );
  }

  /// `Remote Consultations & Smart Tracking`
  String get remote_consult_smart_track {
    return Intl.message(
      'Remote Consultations & Smart Tracking',
      name: 'remote_consult_smart_track',
      desc: '',
      args: [],
    );
  }

  /// `Chat with your doctor, send test results, and receive updated prescriptions — all in a few taps`
  String get chat_send_receive {
    return Intl.message(
      'Chat with your doctor, send test results, and receive updated prescriptions — all in a few taps',
      name: 'chat_send_receive',
      desc: '',
      args: [],
    );
  }

  /// `Invite a family member to stay updated and support you on your health journey.`
  String get invite_family {
    return Intl.message(
      'Invite a family member to stay updated and support you on your health journey.',
      name: 'invite_family',
      desc: '',
      args: [],
    );
  }

  /// `Choose your role to get the experience that fits you best`
  String get choose_role {
    return Intl.message(
      'Choose your role to get the experience that fits you best',
      name: 'choose_role',
      desc: '',
      args: [],
    );
  }

  /// `I’m a Patient`
  String get i_am_patient {
    return Intl.message(
      'I’m a Patient',
      name: 'i_am_patient',
      desc: '',
      args: [],
    );
  }

  /// `I’m a Doctor`
  String get i_am_doctor {
    return Intl.message(
      'I’m a Doctor',
      name: 'i_am_doctor',
      desc: '',
      args: [],
    );
  }

  /// `Sign Up`
  String get sign_up {
    return Intl.message('Sign Up', name: 'sign_up', desc: '', args: []);
  }

  /// `Name`
  String get name {
    return Intl.message('Name', name: 'name', desc: '', args: []);
  }

  /// `enter your name`
  String get enter_name {
    return Intl.message(
      'enter your name',
      name: 'enter_name',
      desc: '',
      args: [],
    );
  }

  /// `Email`
  String get email {
    return Intl.message('Email', name: 'email', desc: '', args: []);
  }

  /// `enter your mail`
  String get enter_email {
    return Intl.message(
      'enter your mail',
      name: 'enter_email',
      desc: '',
      args: [],
    );
  }

  /// `Password`
  String get password {
    return Intl.message('Password', name: 'password', desc: '', args: []);
  }

  /// `enter your password`
  String get enter_password {
    return Intl.message(
      'enter your password',
      name: 'enter_password',
      desc: '',
      args: [],
    );
  }

  /// `Confirm Password`
  String get confirm_password {
    return Intl.message(
      'Confirm Password',
      name: 'confirm_password',
      desc: '',
      args: [],
    );
  }

  /// `Next`
  String get next {
    return Intl.message('Next', name: 'next', desc: '', args: []);
  }

  /// `or Sign Up with`
  String get or_sign_up_with {
    return Intl.message(
      'or Sign Up with',
      name: 'or_sign_up_with',
      desc: '',
      args: [],
    );
  }

  /// `Have an account?`
  String get have_account {
    return Intl.message(
      'Have an account?',
      name: 'have_account',
      desc: '',
      args: [],
    );
  }

  /// `Sign In`
  String get sign_in {
    return Intl.message('Sign In', name: 'sign_in', desc: '', args: []);
  }

  /// `National ID`
  String get national_id {
    return Intl.message('National ID', name: 'national_id', desc: '', args: []);
  }

  /// `Practice License Number`
  String get practice_license_number {
    return Intl.message(
      'Practice License Number',
      name: 'practice_license_number',
      desc: '',
      args: [],
    );
  }

  /// `Upload Syndicate ID Card`
  String get upload_syndicate_id {
    return Intl.message(
      'Upload Syndicate ID Card',
      name: 'upload_syndicate_id',
      desc: '',
      args: [],
    );
  }

  /// `Click to upload the Image`
  String get click_to_upload {
    return Intl.message(
      'Click to upload the Image',
      name: 'click_to_upload',
      desc: '',
      args: [],
    );
  }

  /// `Please upload a clear image of your medical syndicate card`
  String get upload_instruction {
    return Intl.message(
      'Please upload a clear image of your medical syndicate card',
      name: 'upload_instruction',
      desc: '',
      args: [],
    );
  }

  /// `Verify Your Email`
  String get verify_email {
    return Intl.message(
      'Verify Your Email',
      name: 'verify_email',
      desc: '',
      args: [],
    );
  }

  /// `Enter Code`
  String get enter_code {
    return Intl.message('Enter Code', name: 'enter_code', desc: '', args: []);
  }

  /// `we’ve sent an SMS with an activation code to your Email`
  String get email_verification_sent {
    return Intl.message(
      'we’ve sent an SMS with an activation code to your Email',
      name: 'email_verification_sent',
      desc: '',
      args: [],
    );
  }

  /// `Resend Code`
  String get resend_code {
    return Intl.message('Resend Code', name: 'resend_code', desc: '', args: []);
  }

  /// `Don’t have an account? Sign Up`
  String get dont_have_account {
    return Intl.message(
      'Don’t have an account? Sign Up',
      name: 'dont_have_account',
      desc: '',
      args: [],
    );
  }

  /// `Please Enter Your Email Address To Receive a verification code`
  String get enter_email_to_receive_code {
    return Intl.message(
      'Please Enter Your Email Address To Receive a verification code',
      name: 'enter_email_to_receive_code',
      desc: '',
      args: [],
    );
  }

  /// `Try another way`
  String get try_another_way {
    return Intl.message(
      'Try another way',
      name: 'try_another_way',
      desc: '',
      args: [],
    );
  }

  /// `Send`
  String get send {
    return Intl.message('Send', name: 'send', desc: '', args: []);
  }

  /// `Your New Password Must Be Different From previous password`
  String get new_password_note {
    return Intl.message(
      'Your New Password Must Be Different From previous password',
      name: 'new_password_note',
      desc: '',
      args: [],
    );
  }

  /// `Splash`
  String get splash {
    return Intl.message('Splash', name: 'splash', desc: '', args: []);
  }
}

class AppLocalizationDelegate extends LocalizationsDelegate<S> {
  const AppLocalizationDelegate();

  List<Locale> get supportedLocales {
    return const <Locale>[
      Locale.fromSubtags(languageCode: 'en'),
      Locale.fromSubtags(languageCode: 'ar'),
    ];
  }

  @override
  bool isSupported(Locale locale) => _isSupported(locale);
  @override
  Future<S> load(Locale locale) => S.load(locale);
  @override
  bool shouldReload(AppLocalizationDelegate old) => false;

  bool _isSupported(Locale locale) {
    for (var supportedLocale in supportedLocales) {
      if (supportedLocale.languageCode == locale.languageCode) {
        return true;
      }
    }
    return false;
  }
}
