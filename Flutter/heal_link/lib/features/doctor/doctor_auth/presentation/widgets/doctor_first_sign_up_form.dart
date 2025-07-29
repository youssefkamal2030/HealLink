import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/core/widgets/text_field_part.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorFirstSignupForm extends StatefulWidget {
  const DoctorFirstSignupForm({super.key});

  @override
  State<DoctorFirstSignupForm> createState() => _DoctorFirstSignupFormState();
}

class _DoctorFirstSignupFormState extends State<DoctorFirstSignupForm> {
  final _formKey = GlobalKey<FormState>();

  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _confirmPasswordController =
      TextEditingController();

  @override
  void dispose() {
    _passwordController.dispose();
    _confirmPasswordController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Form(
        key: _formKey,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            //* Name TextField
            TextFieldPart(
              titleText: S.of(context).name,
              hintText: S.of(context).enter_name,
              iconPath: AppImages.userPersonIcon,
              isPassword: false,
              customValidator: (value) {
                if (value == null || value.trim().isEmpty) {
                  return 'Name is required';
                } else if (value.trim().length < 2) {
                  return 'Name must be at least 2 characters';
                }
                return null;
              },
            ),

            //* Email TextField
            TextFieldPart(
              titleText: S.of(context).email,
              hintText: S.of(context).enter_email,
              iconPath: AppImages.smsIcon,
              isPassword: false,
              customValidator: (value) {
                final emailRegex = RegExp(r'^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$');
                if (value == null || value.trim().isEmpty) {
                  return 'Email is required';
                } else if (!emailRegex.hasMatch(value.trim())) {
                  return 'Enter a valid email';
                }
                return null;
              },
            ),

            //* Password TextField
            TextFieldPart(
              titleText: S.of(context).password,
              hintText: S.of(context).enter_password,
              iconPath: AppImages.keyIcon,
              isPassword: true,
              controller: _passwordController,
              customValidator: (value) {
                if (value == null || value.isEmpty) {
                  return 'Password is required';
                } else if (value.length < 8) {
                  return 'Password must be at least 8 characters';
                }
                return null;
              },
            ),

            //* Confirm Password
            TextFieldPart(
              titleText: S.of(context).confirm_password,
              hintText: S.of(context).enter_password,
              iconPath: AppImages.keyIcon,
              isPassword: true,
              controller: _confirmPasswordController,
              customValidator: (value) {
                if (value == null || value.isEmpty) {
                  return 'Please confirm your password';
                } else if (value != _passwordController.text) {
                  return 'Passwords do not match';
                }
                return null;
              },
            ),

            const SizedBox(height: 16),

            //* Elevated Button
            CustomElevatedButton(
              onPressed: () {
                if (_formKey.currentState!.validate()) {
                  context.push(AppRouter.doctorSignUpSecondView);
                }
              },
              text: S.of(context).next,
            ),
          ],
        ),
      ),
    );
  }
}
