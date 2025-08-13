
// import 'package:flutter/material.dart';
// import 'package:heal_link/core/utils/app_images.dart';
// import 'package:heal_link/core/widgets/custom_elevated_button.dart';
// import 'package:heal_link/core/widgets/text_field_part.dart';
// import 'package:heal_link/generated/l10n.dart';

// class DoctorResetPasswordForm extends StatelessWidget {
//   const DoctorResetPasswordForm({
//     super.key,
//   });

//   @override
//   Widget build(BuildContext context) {
//     return SliverToBoxAdapter(
//       child: Form(
//         child: Column(
//           children: [
//             //* Email Text Field
//             TextFieldPart(
//               titleText: S.of(context).password,
//               hintText: S.of(context).enter_password,
//               iconPath: AppImages.keyIcon,
//               isPassword: true,
//             ),
//             SizedBox(height: 16),
//             //* Confirm Password TextFeild
//             TextFieldPart(
//               titleText: S.of(context).confirm_password,
//               hintText: S.of(context).enter_password,
//               iconPath: AppImages.keyIcon,
//               isPassword: true,
//             ),
//             SizedBox(height: 32),
    
//             CustomElevatedButton(
//               text: S.of(context).send,
//               onPressed: () {},
//             ),
//           ],
//         ),
//       ),
//     );
//   }
// }





import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/core/widgets/text_field_part.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorResetPasswordForm extends StatefulWidget {
  const DoctorResetPasswordForm({super.key});

  @override
  State<DoctorResetPasswordForm> createState() => _DoctorResetPasswordFormState();
}

class _DoctorResetPasswordFormState extends State<DoctorResetPasswordForm> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _confirmPasswordController = TextEditingController();

  @override
  void dispose() {
    _passwordController.dispose();
    _confirmPasswordController.dispose();
    super.dispose();
  }

  void _submit() {
    if (_formKey.currentState!.validate()) {
      // Submit logic here (e.g., call API or navigate)
    }
  }

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Form(
        key: _formKey,
        child: Column(
          children: [
            //* Password Text Field
            TextFieldPart(
              controller: _passwordController,
              titleText: S.of(context).password,
              hintText: S.of(context).enter_password,
              iconPath: AppImages.keyIcon,
              isPassword: true,
              customValidator: (value) {
                if (value == null || value.isEmpty) {
                  // return S.of(context).please_enter_password;
                  return "Please enter password";
                }
                if (value.length < 6) {
                  // return S.of(context).password_must_be_at_least_6_characters;
                  return "Password must be at least 6 characters";
                }
                return null;
              },
            ),

            const SizedBox(height: 16),

            //* Confirm Password Text Field
            TextFieldPart(
              controller: _confirmPasswordController,
              titleText: S.of(context).confirm_password,
              hintText: S.of(context).enter_password,
              iconPath: AppImages.keyIcon,
              isPassword: true,
              customValidator: (value) {
                if (value == null || value.isEmpty) {
                  // return S.of(context).please_enter_password;
                  return "Please enter password";
                }
                if (value != _passwordController.text) {
                  // return S.of(context).passwords_do_not_match;
                  return "Passwords do not match";
                }
                return null;
              },
            ),

            const SizedBox(height: 32),

            CustomElevatedButton(
              text: S.of(context).send,
              onPressed: _submit,
            ),
          ],
        ),
      ),
    );
  }
}
