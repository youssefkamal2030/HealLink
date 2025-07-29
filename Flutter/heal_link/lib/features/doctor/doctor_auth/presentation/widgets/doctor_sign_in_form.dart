
// import 'package:flutter/material.dart';
// import 'package:go_router/go_router.dart';
// import 'package:heal_link/core/utils/app_images.dart';
// import 'package:heal_link/core/utils/app_router.dart';
// import 'package:heal_link/core/widgets/custom_elevated_button.dart';
// import 'package:heal_link/core/widgets/text_field_part.dart';
// import 'package:heal_link/generated/l10n.dart';

// class DoctorSignInForm extends StatelessWidget {
//   const DoctorSignInForm({
//     super.key,
//   });

//   @override
//   Widget build(BuildContext context) {
//     return SliverToBoxAdapter(
//       child: Form(
//         child: Column(
//           children: [
//             //* Email TextField
//             TextFieldPart(
//               titleText: S.of(context).email,
//               hintText: S.of(context).enter_email,
//               iconPath: AppImages.smsIcon,
//             ),
//             //* Password TextField
//             TextFieldPart(
//               titleText: S.of(context).password,
//               hintText: S.of(context).enter_password,
//               iconPath: AppImages.keyIcon,
//               isPassword: true,
//               isSignIn: true,
//               onForgetPass: (){
//                    context.push(AppRouter.doctorForgetPasswordView);
//               },
//             ),
//             SizedBox(height: 32),
//             CustomElevatedButton(
//               text: S.of(context).sign_in,
//               onPressed: () {
//                     context.push(AppRouter.doctorHomeView,
                    
//                     );
//               },
//             ),
//           ],
//         ),
//       ),
//     );
//   }
// }




import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/core/widgets/text_field_part.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignInForm extends StatefulWidget {
  const DoctorSignInForm({super.key});

  @override
  State<DoctorSignInForm> createState() => _DoctorSignInFormState();
}

class _DoctorSignInFormState extends State<DoctorSignInForm> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  @override
  void dispose() {
    _emailController.dispose();
    _passwordController.dispose();
    super.dispose();
  }

  void _submit() {
    if (_formKey.currentState!.validate()) {
      context.push(AppRouter.doctorHomeView);
    }
  }

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Form(
        key: _formKey,
        child: Column(
          children: [
            //* Email TextField
            TextFieldPart(
              controller: _emailController,
              titleText: S.of(context).email,
              hintText: S.of(context).enter_email,
              iconPath: AppImages.smsIcon,
              customValidator: (value) {
                if (value == null || value.isEmpty) {
                  // return S.of(context).please_enter_email;
                  return "Please enter email";

                }
                return null;
              },
            ),

            //* Password TextField
            TextFieldPart(
              controller: _passwordController,
              titleText: S.of(context).password,
              hintText: S.of(context).enter_password,
              iconPath: AppImages.keyIcon,
              isPassword: true,
              isSignIn: true,
              onForgetPass: () {
                context.push(AppRouter.doctorForgetPasswordView);
              },
              customValidator: (value) {
                if (value == null || value.isEmpty) {
                  //return S.of(context).please_enter_password;
                  return "Please enter password";
                }
                return null;
              },
            ),

            const SizedBox(height: 32),

            CustomElevatedButton(
              text: S.of(context).sign_in,
              onPressed: _submit,
            ),
          ],
        ),
      ),
    );
  }
}
