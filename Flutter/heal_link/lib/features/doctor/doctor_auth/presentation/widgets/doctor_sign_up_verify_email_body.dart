import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_logo.dart';
// ignore: unused_import
import 'package:heal_link/core/widgets/custom_otp_input_row.dart';
import 'package:heal_link/core/widgets/custom_sliver_height_16.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_verify_code_message.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_verify_email_button.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_verify_email_enter_code.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_verify_email_header.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_verify_email_otp_row.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_verify_email_resend_code.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_verify_email_time_widget.dart';

class DoctorSignUpVerifyEmailBody extends StatelessWidget {
  const DoctorSignUpVerifyEmailBody({super.key});

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: EdgeInsets.symmetric(horizontal: 16),
        child: CustomScrollView(
          slivers: [
            DoctorVerifyEmailHeader(),
            CustomSliverHeight(32),
            CustomAppLogo(),
            CustomSliverHeight(32),

            DoctorVerifyEmailEnterCode(),
            CustomSliverHeight(24),
            DoctorVerifyCodeMessage(),
            CustomSliverHeight(32),
            DoctorVerifyEmailOtpRow(),
            CustomSliverHeight(24),
            DoctorVerifyEmailTimeWidget(),
            CustomSliverHeight(8),
            DoctorVerifyEmailResendCode(),
            CustomSliverHeight(32),
            DoctorVerifyEmailButton(),
          ],
        ),
      ),
    );
  }
}







/**
 * 
 


 //! Doctor Verify Email Form 


class DoctorVerifyEmailForm extends StatelessWidget {
  const DoctorVerifyEmailForm({super.key});

  @override
  Widget build(BuildContext context) {
    return Form(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
           
            DoctorVerifyEmailEnterCode(),
            SizedBox(height: 24,) , 
            DoctorVerifyCodeMessage(),
            SizedBox(height: 32,) , 
            DoctorVerifyEmailOtpRow(),
            SizedBox(height: 24,) , 
            DoctorVerifyEmailTimeWidget(),
            SizedBox(height: 8,) , 
            DoctorVerifyEmailResendCode(),
            SizedBox(height: 32,) , 
            DoctorVerifyEmailButton()
        ],
      ),
    );
  }
}


 */