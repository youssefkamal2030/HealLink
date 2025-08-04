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

  // skipped getter for the 'total_patient>' key

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

  /// `Patient’s Results`
  String get patient_results {
    return Intl.message(
      'Patient’s Results',
      name: 'patient_results',
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

  /// `Payment Methods`
  String get payment_methods {
    return Intl.message('Payment Methods', name: 'payment_methods', desc: '', args: []);
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

  /// `Messages`
  String get messages {
    return Intl.message('Messages', name: 'messages', desc: '', args: []);
  }

  /// `My Profile`
  String get my_profile {
    return Intl.message('My Profile', name: 'my_profile', desc: '', args: []);
  }

  /// `No chat at the moment.`
  String get no_chat_at_moment {
    return Intl.message(
      'No chat at the moment.',
      name: 'no_chat_at_moment',
      desc: '',
      args: [],
    );
  }

  /// `Once patients start chatting with you, Their conversation will appear here.`
  String get once_patients_chatting {
    return Intl.message(
      'Once patients start chatting with you, Their conversation will appear here.',
      name: 'once_patients_chatting',
      desc: '',
      args: [],
    );
  }

  /// `All`
  String get all {
    return Intl.message('All', name: 'all', desc: '', args: []);
  }

  /// `Unread`
  String get unread {
    return Intl.message('Unread', name: 'unread', desc: '', args: []);
  }

  /// `Change Profile`
  String get change_profile {
    return Intl.message(
      'Change Profile',
      name: 'change_profile',
      desc: '',
      args: [],
    );
  }

  /// `Search for a patient`
  String get search_for_a_patient {
    return Intl.message(
      'Search for a patient',
      name: 'search_for_a_patient',
      desc: '',
      args: [],
    );
  }

  /// `Prescriptions`
  String get prescriptions {
    return Intl.message(
      'Prescriptions',
      name: 'prescriptions',
      desc: '',
      args: [],
    );
  }

  /// `Uploaded Reports & Files`
  String get uploaded_reports_files {
    return Intl.message(
      'Uploaded Reports & Files',
      name: 'uploaded_reports_files',
      desc: '',
      args: [],
    );
  }

  /// `Lab Tests`
  String get lab_tests {
    return Intl.message('Lab Tests', name: 'lab_tests', desc: '', args: []);
  }

  /// `X-rays`
  String get x_rays {
    return Intl.message('X-rays', name: 'x_rays', desc: '', args: []);
  }

  /// `Patient History`
  String get patient_history {
    return Intl.message(
      'Patient History',
      name: 'patient_history',
      desc: '',
      args: [],
    );
  }

  /// `Chronic diseases`
  String get chronic_diseases {
    return Intl.message(
      'Chronic diseases',
      name: 'chronic_diseases',
      desc: '',
      args: [],
    );
  }

  /// `Allergies`
  String get allergies {
    return Intl.message('Allergies', name: 'allergies', desc: '', args: []);
  }

  /// `Past Surgeries`
  String get past_surgeries {
    return Intl.message(
      'Past Surgeries',
      name: 'past_surgeries',
      desc: '',
      args: [],
    );
  }

  /// `Current Medications`
  String get current_medications {
    return Intl.message(
      'Current Medications',
      name: 'current_medications',
      desc: '',
      args: [],
    );
  }

  /// `Search for a patient`
  String get searchPatient {
    return Intl.message(
      'Search for a patient',
      name: 'searchPatient',
      desc: '',
      args: [],
    );
  }

  /// `Last interaction: `
  String get lastInteraction {
    return Intl.message(
      'Last interaction: ',
      name: 'lastInteraction',
      desc: '',
      args: [],
    );
  }

  /// `Type: `
  String get type {
    return Intl.message('Type: ', name: 'type', desc: '', args: []);
  }

  /// `View Details`
  String get viewDetails {
    return Intl.message(
      'View Details',
      name: 'viewDetails',
      desc: '',
      args: [],
    );
  }

  /// `Total Patient`
  String get total_patient {
    return Intl.message(
      'Total Patient',
      name: 'total_patient',
      desc: '',
      args: [],
    );
  }

  /// `Accept`
  String get accept {
    return Intl.message('Accept', name: 'accept', desc: '', args: []);
  }

  /// `Reject`
  String get reject {
    return Intl.message('Reject', name: 'reject', desc: '', args: []);
  }

  /// `Gender`
  String get gender {
    return Intl.message('Gender', name: 'gender', desc: '', args: []);
  }

  /// `Age`
  String get age {
    return Intl.message('Age', name: 'age', desc: '', args: []);
  }

  /// `View Prescription`
  String get viewPrescription {
    return Intl.message(
      'View Prescription',
      name: 'viewPrescription',
      desc: '',
      args: [],
    );
  }

  /// `Add Prescription`
  String get addPrescription {
    return Intl.message(
      'Add Prescription',
      name: 'addPrescription',
      desc: '',
      args: [],
    );
  }

  /// `Basic Info`
  String get basicInfo {
    return Intl.message('Basic Info', name: 'basicInfo', desc: '', args: []);
  }

  /// `Full Name`
  String get fullName {
    return Intl.message('Full Name', name: 'fullName', desc: '', args: []);
  }

  /// `Nationality`
  String get nationality {
    return Intl.message('Nationality', name: 'nationality', desc: '', args: []);
  }

  /// `Contact Info`
  String get contactInfo {
    return Intl.message(
      'Contact Info',
      name: 'contactInfo',
      desc: '',
      args: [],
    );
  }

  /// `Phone Number`
  String get phoneNumber {
    return Intl.message(
      'Phone Number',
      name: 'phoneNumber',
      desc: '',
      args: [],
    );
  }

  /// `Enter your email address`
  String get emailAddress {
    return Intl.message(
      'Enter your email address',
      name: 'emailAddress',
      desc: '',
      args: [],
    );
  }

  /// `Address`
  String get address {
    return Intl.message('Address', name: 'address', desc: '', args: []);
  }

  /// `Professional Info`
  String get professionalInfo {
    return Intl.message(
      'Professional Info',
      name: 'professionalInfo',
      desc: '',
      args: [],
    );
  }

  /// `Medical License Number`
  String get medicalLicenseNumber {
    return Intl.message(
      'Medical License Number',
      name: 'medicalLicenseNumber',
      desc: '',
      args: [],
    );
  }

  /// `Current Workplace / Hospital`
  String get currentWorkplace {
    return Intl.message(
      'Current Workplace / Hospital',
      name: 'currentWorkplace',
      desc: '',
      args: [],
    );
  }

  /// `This Month`
  String get thisMonth {
    return Intl.message('This Month', name: 'thisMonth', desc: '', args: []);
  }

  /// `Specialization`
  String get specialization {
    return Intl.message(
      'Specialization',
      name: 'specialization',
      desc: '',
      args: [],
    );
  }

  /// `Add`
  String get add {
    return Intl.message('Add', name: 'add', desc: '', args: []);
  }

  /// `Date`
  String get date {
    return Intl.message('Date', name: 'date', desc: '', args: []);
  }

  /// `Medicine Name`
  String get medicine_name {
    return Intl.message(
      'Medicine Name',
      name: 'medicine_name',
      desc: '',
      args: [],
    );
  }

  /// `Frequency of Use`
  String get frequency_of_use {
    return Intl.message(
      'Frequency of Use',
      name: 'frequency_of_use',
      desc: '',
      args: [],
    );
  }

  /// `Usage Instructions`
  String get usage_instructions {
    return Intl.message(
      'Usage Instructions',
      name: 'usage_instructions',
      desc: '',
      args: [],
    );
  }

  /// `notifications`
  String get notifications {
    return Intl.message(
      'notifications',
      name: 'notifications',
      desc: '',
      args: [],
    );
  }

  /// `enter your national ID`
  String get enter_national_id {
    return Intl.message(
      'enter your national ID',
      name: 'enter_national_id',
      desc: '',
      args: [],
    );
  }

  /// `Search`
  String get search {
    return Intl.message('Search', name: 'search', desc: '', args: []);
  }

  /// `Search History`
  String get search_history {
    return Intl.message(
      'Search History',
      name: 'search_history',
      desc: '',
      args: [],
    );
  }

  /// `please enter national Id `
  String get please_enter_national_id {
    return Intl.message(
      'please enter national Id ',
      name: 'please_enter_national_id',
      desc: '',
      args: [],
    );
  }

  /// `national id must be 14 digits`
  String get national_id_must_be_14_digits {
    return Intl.message(
      'national id must be 14 digits',
      name: 'national_id_must_be_14_digits',
      desc: '',
      args: [],
    );
  }

  /// `please enter practice license number`
  String get please_enter_practice_license_number {
    return Intl.message(
      'please enter practice license number',
      name: 'please_enter_practice_license_number',
      desc: '',
      args: [],
    );
  }

  /// `license nubmer too short`
  String get license_number_too_short {
    return Intl.message(
      'license nubmer too short',
      name: 'license_number_too_short',
      desc: '',
      args: [],
    );
  }

  /// `Account`
  String get account {
    return Intl.message('Account', name: 'account', desc: '', args: []);
  }

  /// `Account Info`
  String get account_info {
    return Intl.message(
      'Account Info',
      name: 'account_info',
      desc: '',
      args: [],
    );
  }

  /// `Privacy & Security`
  String get privacy_security {
    return Intl.message(
      'Privacy & Security',
      name: 'privacy_security',
      desc: '',
      args: [],
    );
  }

  /// `Data & Storage`
  String get data_storage {
    return Intl.message(
      'Data & Storage',
      name: 'data_storage',
      desc: '',
      args: [],
    );
  }

  /// `Notification Preferences`
  String get notification_preferences {
    return Intl.message(
      'Notification Preferences',
      name: 'notification_preferences',
      desc: '',
      args: [],
    );
  }

  /// `Language`
  String get language {
    return Intl.message('Language', name: 'language', desc: '', args: []);
  }

  /// `App Version & Updates`
  String get app_version_updates {
    return Intl.message(
      'App Version & Updates',
      name: 'app_version_updates',
      desc: '',
      args: [],
    );
  }

  /// `Two-Factor Authentication`
  String get two_factor_authentication {
    return Intl.message(
      'Two-Factor Authentication',
      name: 'two_factor_authentication',
      desc: '',
      args: [],
    );
  }

  /// `Deactivate Account`
  String get deactivate_account {
    return Intl.message(
      'Deactivate Account',
      name: 'deactivate_account',
      desc: '',
      args: [],
    );
  }

  /// `Delete Account`
  String get delete_account {
    return Intl.message(
      'Delete Account',
      name: 'delete_account',
      desc: '',
      args: [],
    );
  }

  /// `Who can view my profile`
  String get who_can_view_my_profile {
    return Intl.message(
      'Who can view my profile',
      name: 'who_can_view_my_profile',
      desc: '',
      args: [],
    );
  }

  /// `Login Alerts`
  String get login_alerts {
    return Intl.message(
      'Login Alerts',
      name: 'login_alerts',
      desc: '',
      args: [],
    );
  }

  /// `Biometric Authentication`
  String get biometric_authentication {
    return Intl.message(
      'Biometric Authentication',
      name: 'biometric_authentication',
      desc: '',
      args: [],
    );
  }

  /// `Show my Availability Status`
  String get show_my_availability_status {
    return Intl.message(
      'Show my Availability Status',
      name: 'show_my_availability_status',
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
