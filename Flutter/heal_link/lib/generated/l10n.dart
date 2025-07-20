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

  /// `Skip`
  String get skip {
    return Intl.message('Skip', name: 'skip', desc: '', args: []);
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

  /// `Don’t have an account?`
  String get dont_have_account {
    return Intl.message(
      'Don’t have an account?',
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

  /// `Available now`
  String get available_now {
    return Intl.message(
      'Available now',
      name: 'available_now',
      desc: '',
      args: [],
    );
  }

  /// `Total Patient >`
  String get total_patient {
    return Intl.message(
      'Total Patient >',
      name: 'total_patient',
      desc: '',
      args: [],
    );
  }

  /// `Members`
  String get members {
    return Intl.message('Members', name: 'members', desc: '', args: []);
  }

  /// `Requests`
  String get requests {
    return Intl.message('Requests', name: 'requests', desc: '', args: []);
  }

  /// `Recent Patient`
  String get recent_patient {
    return Intl.message(
      'Recent Patient',
      name: 'recent_patient',
      desc: '',
      args: [],
    );
  }

  /// `See all`
  String get see_all {
    return Intl.message('See all', name: 'see_all', desc: '', args: []);
  }

  /// `Home`
  String get home {
    return Intl.message('Home', name: 'home', desc: '', args: []);
  }

  /// `Message`
  String get message {
    return Intl.message('Message', name: 'message', desc: '', args: []);
  }

  /// 'Change Profile
  String get change_profile {
    return Intl.message(
      'Change Profile',
      name: 'change_profile',
      desc: '',
      args: [],
    );
  }

  /// `Personal Information`
  String get personal_information {
    return Intl.message(
      'Personal Information',
      name: 'personal_information',
      desc: '',
      args: [],
    );
  }

  /// `Notification`
  String get notification {
    return Intl.message(
      'Notification',
      name: 'notification',
      desc: '',
      args: [],
    );
  }

  /// `Payment`
  String get payment {
    return Intl.message('Payment', name: 'payment', desc: '', args: []);
  }

  /// `Terms & Privacy Policy`
  String get terms_and_privacy_policy {
    return Intl.message(
      'Terms & Privacy Policy',
      name: 'terms_and_privacy_policy',
      desc: '',
      args: [],
    );
  }

  /// `Support Help`
  String get support_help {
    return Intl.message(
      'Support Help',
      name: 'support_help',
      desc: '',
      args: [],
    );
  }

  /// `Setting`
  String get setting {
    return Intl.message('Setting', name: 'setting', desc: '', args: []);
  }

  /// `About`
  String get about {
    return Intl.message('About', name: 'about', desc: '', args: []);
  }

  /// `Subscription`
  String get subscription {
    return Intl.message(
      'Subscription',
      name: 'subscription',
      desc: '',
      args: [],
    );
  }

  /// `Profile`
  String get profile {
    return Intl.message('Profile', name: 'profile', desc: '', args: []);
  }

  /// `No patients at the moment.`
  String get no_patients_at_moment {
    return Intl.message(
      'No patients at the moment.',
      name: 'no_patients_at_moment',
      desc: '',
      args: [],
    );
  }

  /// `Once patients start subscribing with you, their information will appear here.`
  String get once_patients_subscribing {
    return Intl.message(
      'Once patients start subscribing with you, their information will appear here.',
      name: 'once_patients_subscribing',
      desc: '',
      args: [],
    );
  }

  /// `Messages`
  String get messages {
    return Intl.message('Messages', name: 'messages', desc: '', args: []);
  }

  /// `Profile`
  String get my_profile {
    return Intl.message('My Profile', name: 'my_profile', desc: '', args: []);
  }

  /// `No messages at the moment.`
  String get no_chat_at_moment {
    return Intl.message(
      'No chat at the moment.',
      name: 'no_chat_at_moment',
      desc: '',
      args: [],
    );
  }

  // ` Once patients start chatting with you, Their conversation will appear here.`
  String get once_patients_chatting {
    return Intl.message(
      ' Once patients start chatting with you, Their conversation will appear here.',
      name: 'once_patients_chatting',
      desc: '',
      args: [],
    );
  }

  // all chat
  String get all {
    return Intl.message('All', name: 'all', desc: '', args: []);
  }

  // unread chat
  String get unread {
    return Intl.message('Unread', name: 'unread', desc: '', args: []);
  }

  /// search for a patient
  String get search_for_a_patient {
    return Intl.message(
      'Search for a patient',
      name: 'search_for_a_patient',
      desc: '',
      args: [],
    );
  }

  /// `Patient’s Results`
  String get patient_results {
    return Intl.message(
      'Patient’s Results',
      name: 'patient_results',
      desc: '',
      args: [],
    );
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
