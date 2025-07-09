
import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_logo.dart';
import 'package:heal_link/core/widgets/custom_sliver_height_16.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_reset_password_form.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_reset_password_header.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_reset_password_message.dart';

class DoctorResetPasswordBody extends StatelessWidget {
  const DoctorResetPasswordBody({super.key});

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: EdgeInsetsGeometry.symmetric(horizontal: 16),
        child: CustomScrollView(
          slivers: [
            DoctorResetPasswordHeader(),
            CustomSliverHeight(32),
            CustomAppLogo(),
            CustomSliverHeight(32),
            DoctorResetPasswordMessage(),
            CustomSliverHeight(24),
            DoctorResetPasswordForm(),
          ],
        ),
      ),
    );
  }
}
